#nullable enable
using AutoMapper;
using BLL;
using BLL.Contracts.Responses;
using BLL.Services;
using Domain;
using Domain.Enums;
using DrinkManager.API.Extensions;
using DrinkManager.API.Models.ViewModels;
using DrinkManager.API.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Persistence.Repositories;
using ReportingModuleApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkManager.API.Controllers
{
    public class DrinksController : BaseController
    {
        private readonly IDrinkRepository _drinkRepository;
        private readonly IDrinkSearchService _drinkSearchService;
        private readonly IReportingModuleService _apiService;
        private readonly IFavouriteRepository _favouriteRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IStringLocalizer<SharedResource> _localizer;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly int _pageSize = 12;

        public DrinksController(
            IDrinkRepository drinkRepository,
            IDrinkSearchService drinkSearchService,
            IFavouriteRepository favouriteRepository,
            IReviewRepository reviewRepository,
            UserManager<AppUser> userManager,
            IReportingModuleService apiService,
            IStringLocalizer<SharedResource> localizer,
            IMapper mapper)
        {
            _drinkRepository = drinkRepository;
            _drinkSearchService = drinkSearchService;
            _favouriteRepository = favouriteRepository;
            _reviewRepository = reviewRepository;
            _userManager = userManager;
            _localizer = localizer;
            _mapper = mapper;
            _apiService = apiService;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] int? page)
        {
            Task.Run(() => _apiService.CreateUserActivity(PerformedAction.AllDrinks, User.Identity.Name)).Forget();

            var drinks = await PaginatedList<Drink>.CreateAsync(_drinkRepository.GetAllDrinks(), page ?? 1, _pageSize);

            return Ok(new { drinks = drinks.Select(_mapper.Map<GetDrinkListResponse>), totalPages = drinks.TotalPages });
        }


        [HttpGet("drink/{id}")]
        public async Task<IActionResult> DrinkDetails(string id)
        {
            var drink = await _drinkRepository.GetDrinkById(id);
            Task.Run(() => _apiService.CreateUserActivity(PerformedAction.VisitedDrink, User.Identity.Name, drinkId: id, drinkName: drink.Name)).Forget();
            if (drink == null)
            {
                // add error View
            }

            var model = new DrinkDetailsViewModel
            {
                Drink = drink,
                IsFavourite = _favouriteRepository.IsFavourite(_userManager.GetUserId(User), drink?.DrinkId),
                CanUserReview = _reviewRepository.CanUserReviewDrink(_userManager.GetUserId(User), drink?.DrinkId)
            };
            return Ok(model);
        }

        [Authorize]
        [HttpGet("Drinks/favourites")]
        public async Task<IActionResult> FavouriteDrinks(int? pageNumber)
        {
            var drinks = _favouriteRepository.GetUserFavouriteDrinks(_userManager.GetUserId(User));

            var model = new DrinksViewModel
            {
                Drinks = await PaginatedList<Drink>.CreateAsync(drinks, pageNumber ?? 1, _pageSize)
            };
            return Ok(model);
        }

        // [HttpGet("drink/edit/{id}")]
        // public async Task<IActionResult> Edit(string? id)
        // {
        //     var drink = await _drinkRepository.GetDrinkById(id);
        //
        //     var model = new DrinkCreateViewModel
        //     {
        //         Id = drink?.DrinkId,
        //         GlassType = drink?.GlassType,
        //         Category = drink?.Category,
        //         Instructions = drink?.Instructions,
        //         AlcoholicInfo = drink?.AlcoholicInfo,
        //         Name = drink?.Name,
        //         Ingredients = drink?.Ingredients,
        //         ImageUrl = drink?.ImageUrl
        //     };
        //
        //     return Ok("Create", model);
        // }

        [HttpGet("drink/create")]
        public IActionResult Create()
        {
            return Ok();
        }

        [HttpPost("drink/create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormCollection data, string? id)
        {
            if (!ModelState.IsValid)
            {
                return Ok();
            }

            Task.Run(() =>
                _apiService.CreateUserActivity(PerformedAction.EditOrCreateDrink, this.User.Identity.Name, id,
                    data["Name"])).Forget();
            var ingredients = new List<Ingredient>();

            // create ingredient objects from the from data
            foreach (var key in data.Keys)
            {
                if (key.Contains("Ingredient"))
                {
                    ingredients.Add(new Ingredient
                    {
                        Name = data[key],
                        Amount = data["Amount" + key.Split("Ingredient")[1]]
                    });
                }
            }

            // image placeholder
            var imageUrl = "https://medifactia.com/wp-content/uploads/2018/01/placeholder.png";

            // if image data exists replace placeholder
            if (data.ContainsKey("ImageUrl") && string.IsNullOrWhiteSpace(data["ImageUrl"]) == false)
            {
                imageUrl = data["ImageUrl"];
            }

            // id that we will use for the redirect
            string redirectId;

            if (id != null)
            {
                // ID is not null, we edit
                var drinkToUpdate = await _drinkRepository.GetDrinkById(id);


                if (drinkToUpdate == null)
                {
                    // something went wrong redirect to drinks index
                    // TempData["Alert"] = _localizer["DrinkNotFound"] + ".";
                    // TempData["AlertClass"] = "alert-danger";

                    return RedirectToAction(nameof(Index));
                }

                drinkToUpdate.Ingredients = ingredients;
                drinkToUpdate.GlassType = data["GlassType"];
                drinkToUpdate.Category = data["Category"];
                drinkToUpdate.AlcoholicInfo = data["AlcoholicInfo"];
                drinkToUpdate.Instructions = data["Instructions"];
                drinkToUpdate.Name = data["Name"];
                drinkToUpdate.ImageUrl = imageUrl;
                _drinkRepository.Update(drinkToUpdate);
                redirectId = id;
            }
            else
            {
                // id was null, we create a new drink
                var newDrink = new Drink
                {
                    DrinkId = Guid.NewGuid().ToString(),
                    Ingredients = ingredients,
                    GlassType = data["GlassType"],
                    ImageUrl = imageUrl,
                    DrinkReviews = new List<DrinkReview>(),
                    Category = data["Category"],
                    AlcoholicInfo = data["AlcoholicInfo"],
                    Instructions = data["Instructions"],
                    Name = data["Name"]
                };

                await _drinkRepository.AddDrink(newDrink);
                redirectId = newDrink.DrinkId;
            }

            await _drinkRepository.SaveChanges();

            return RedirectToAction(nameof(DrinkDetails), new { id = redirectId });
        }


        public async Task<IActionResult> Remove(string id)
        {
            var drink = await _drinkRepository.GetDrinkById(id);
            Task.Run(() =>
                _apiService.CreateUserActivity(PerformedAction.RemoveDrink, this.User.Identity.Name, id, drink.Name)).Forget();
            if (drink == null)
            {
                return NotFound();
            }

            _drinkRepository.DeleteDrink(drink);
            await _drinkRepository.SaveChanges();

            // TempData["Alert"] = $"Drink {drink.Name} " + _localizer["removed"] + ".";
            // TempData["AlertClass"] = "alert-success";

            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> AddToFavourite(string id)
        {
            var drink = await _drinkRepository.GetDrinkById(id);
            Task.Run(() =>
                _apiService.CreateUserActivity(PerformedAction.AddedToFavourite, this.User.Identity.Name, drinkId: id, drinkName: drink.Name)).Forget();
            if (drink == null)
            {
                // add error View
            }

            _favouriteRepository.AddToFavourites(_userManager.GetUserId(User), drink);

            return RedirectToAction("DrinkDetails", new { id });
        }

        [Authorize]
        public async Task<IActionResult> RemoveFromFavourite(string id)
        {
            var drink = await _drinkRepository.GetDrinkById(id);
            Task.Run(() =>
                _apiService.CreateUserActivity(PerformedAction.RemovedFromFavourite, this.User.Identity.Name, id, drink.Name)).Forget();
            if (drink == null)
            {
                // add error View
            }

            _favouriteRepository.RemoveFromFavourites(_userManager.GetUserId(User), drink?.DrinkId);

            return RedirectToAction("DrinkDetails", new { id });
        }

        // [Authorize]
        // [HttpGet("drink/addReview/{id}")]
        // public async Task<IActionResult> AddReview(string? id)
        // {
        //     var drink = await _drinkRepository.GetDrinkById(id);
        //
        //     var model = new DrinkCreateViewModel
        //     {
        //         Name = drink?.Name,
        //         Id = drink?.DrinkId
        //     };
        //
        //     return Ok("AddReview", model);
        // }

        [HttpPost("drink/addReview/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddReview(IFormCollection data, string? id)
        {

            var drinkToUpdate = await _drinkRepository.GetDrinkById(id);
            Task.Run(() =>
                _apiService.CreateUserActivity(PerformedAction.AddedReview, this.User.Identity.Name, id, drinkToUpdate.Name, score: int.Parse(data["DrinkReview.ReviewScore"]))).Forget();
            if (drinkToUpdate == null)
            {
                // TempData["Alert"] = _localizer["DrinkNotFound"] + ".";
                // TempData["AlertClass"] = "alert-danger";
                return RedirectToAction(nameof(Index));
            }

            drinkToUpdate.DrinkReviews.Add(new DrinkReview
            {
                ReviewText = data["DrinkReview.ReviewText"],
                ReviewScore = int.Parse(data["DrinkReview.ReviewScore"]),
                Drink = drinkToUpdate,
                Author = await _userManager.GetUserAsync(User),
                ReviewDate = DateTime.Now
            });
            _drinkRepository.Update(drinkToUpdate);
            await _drinkRepository.SaveChanges();

            return RedirectToAction(nameof(DrinkDetails), new { id });
        }

        [Authorize]
        [HttpGet("Drinks/reviews")]
        public async Task<IActionResult> ReviewedDrinks(int? pageNumber)
        {
            var drinks = _reviewRepository.GetUserReviewedDrinks(_userManager.GetUserId(User));

            var model = new DrinksViewModel
            {
                Drinks = await PaginatedList<Drink>.CreateAsync(drinks, pageNumber ?? 1, _pageSize)
            };
            return Ok(model);
        }

        public async Task<IActionResult> SearchByAlcoholContent(int? pageNumber, bool alcoholics = true, bool nonAlcoholics = true, bool optionalAlcoholics = true)
        {
            // ViewData["Alcoholics"] = alcoholics;
            // ViewData["nonAlcoholics"] = nonAlcoholics;
            // ViewData["optionalAlcoholics"] = optionalAlcoholics;
            var drinks = _drinkSearchService
                .SearchByAlcoholContent(alcoholics, nonAlcoholics, optionalAlcoholics);

            //Model saves alcoholic content info passed by controller to save
            //user choices while going through PaginatedList pages
            var model = new DrinksViewModel
            {
                Drinks = await PaginatedList<Drink>.CreateAsync(drinks, pageNumber ?? 1, _pageSize),
                Alcoholics = alcoholics,
                NonAlcoholics = nonAlcoholics,
                OptionalAlcoholics = optionalAlcoholics
            };
            return Ok(model);
        }

        public async Task<IActionResult> SearchByName(string searchString, int? pageNumber)
        {
            var drinks = _drinkRepository.GetAllDrinks();
            if (!string.IsNullOrEmpty(searchString))
            {
                Task.Run(() =>
                    _apiService.CreateUserActivity(PerformedAction.SearchByName, this.User.Identity.Name, searchedPhrase: searchString)).Forget();
                drinks = _drinkSearchService.SearchByName(searchString);
            }

            // ViewData["SearchString"] = searchString;
            // ViewData["SearchType"] = "SearchByName";

            var model = new DrinksViewModel
            {
                Drinks = await PaginatedList<Drink>.CreateAsync(drinks,
                    pageNumber ?? 1, _pageSize)
            };
            return Ok(model);
        }

        public async Task<IActionResult> SearchByIngredients(string searchString, int? pageNumber,
            string searchCondition = "any")
        {
            var drinks = _drinkRepository.GetAllDrinks();

            if (!string.IsNullOrEmpty(searchString))
            {
                Task.Run(() =>
                    _apiService.CreateUserActivity(PerformedAction.SearchByIngredients, this.User.Identity.Name, searchedPhrase: searchString)).Forget();
                var searchDrinkIngredientsCondition =
                    searchCondition.Equals("all") ? SearchDrinkOption.All : SearchDrinkOption.Any;
                drinks = _drinkSearchService.SearchByIngredients(new SortedSet<string>(searchString.Split(' ')),
                    searchDrinkIngredientsCondition);
            }

            // ViewData["SearchString"] = searchString;
            // ViewData["SearchCondition"] = searchCondition;
            // ViewData["SearchType"] = "SearchByIngredients";


            var model = new DrinksViewModel
            {
                Drinks = await PaginatedList<Drink>.CreateAsync(drinks,
                    pageNumber ?? 1, _pageSize)
            };
            return Ok(model);
        }
    }
}