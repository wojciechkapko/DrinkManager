#nullable enable
using AutoMapper;
using BLL;
using BLL.Contracts.Requests;
using BLL.Contracts.Responses;
using BLL.Services;
using Domain;
using Domain.Enums;
using DrinkManager.API.Extensions;
using DrinkManager.API.Models.ViewModels;
using DrinkManager.API.Resources;
using LazyCache;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Persistence.Repositories;
using ReportingModule.API.Services;
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
        private readonly IAppCache _cache;
        private readonly UserManager<AppUser> _userManager;

        public DrinksController(
            IDrinkRepository drinkRepository,
            IDrinkSearchService drinkSearchService,
            IFavouriteRepository favouriteRepository,
            IReviewRepository reviewRepository,
            UserManager<AppUser> userManager,
            IReportingModuleService apiService,
            IStringLocalizer<SharedResource> localizer,
            IMapper mapper,
            IAppCache cache)
        {
            _drinkRepository = drinkRepository;
            _drinkSearchService = drinkSearchService;
            _favouriteRepository = favouriteRepository;
            _reviewRepository = reviewRepository;
            _userManager = userManager;
            _localizer = localizer;
            _mapper = mapper;
            _cache = cache;
            _apiService = apiService;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] int page = 1, [FromQuery] int pageCount = 12)
        {
            Task.Run(() => _apiService.CreateUserActivity(PerformedAction.AllDrinks, User.Identity.Name)).Forget();

            var drinks = await _cache.GetOrAddAsync(
                $"drinks_page_{page}_pagecount_{pageCount}",
        () => PaginatedList<Drink>.CreateAsync(_drinkRepository.GetAllDrinks(), page,
            pageCount), TimeSpan.MaxValue);


            return Ok(new { drinks = drinks.Select(_mapper.Map<DrinkListResponse>), totalPages = drinks.TotalPages });
        }

        [HttpOptions]
        public IActionResult PreflightRoute([FromQuery] int? page)
        {

            return NoContent();
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> DrinkDetails(string id)
        {
            var drink = await _drinkRepository.GetDrinkById(id);


            if (drink == null)
            {
                return NotFound("Drink not found");
            }

            Task.Run(() => _apiService.CreateUserActivity(PerformedAction.VisitedDrink, User.Identity.Name, drinkId: id, drinkName: drink.Name)).Forget();

            return Ok(_mapper.Map<DrinkDetailsResponse>(drink));
        }


        [HttpGet("{id}/reviews")]
        public async Task<IActionResult> GetDrinkReviews(string id, [FromQuery] int? page, [FromQuery] int? pageCount)
        {
            var reviews = await PaginatedList<DrinkReview>.CreateAsync(_reviewRepository.GetDrinkReviews(id), page ?? 1, pageCount ?? 10);

            return Ok(new { reviews = reviews.Select(_mapper.Map<ReviewResponse>), totalPages = reviews.TotalPages });
        }

        [HttpPost("{id}/reviews")]
        public async Task<IActionResult> AddReview(string id, ReviewCreateRequest request)
        {

            // todo: fix this task

            // Task.Run(() =>
            //     _apiService.CreateUserActivity(PerformedAction.AddedReview, this.User.Identity.Name, id, drinkToUpdate.Name, score: request.ReviewScore)).Forget();

            var newReview = new DrinkReview
            {
                DrinkId = id,
                AuthorName = request.AuthorName,
                ReviewDate = DateTime.Now,
                ReviewScore = int.Parse(request.ReviewScore),
                ReviewText = request.ReviewText
            };

            if (await _reviewRepository.AddReview(newReview) == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }


            return Ok(_mapper.Map<ReviewResponse>(newReview));
        }


        [Authorize]
        [HttpGet("Drinks/favourites")]
        public async Task<IActionResult> FavouriteDrinks(int? pageNumber, [FromQuery] int? pageCount)
        {
            var drinks = _favouriteRepository.GetUserFavouriteDrinks(_userManager.GetUserId(User));

            var model = new DrinksViewModel
            {
                Drinks = await PaginatedList<Drink>.CreateAsync(drinks, pageNumber ?? 1, pageCount ?? 10)
            };
            return Ok(model);
        }

        [HttpGet("drink/edit/{id}")]
        public async Task<IActionResult> Edit(string? id)
        {
            var drink = await _drinkRepository.GetDrinkById(id);

            var model = new DrinkCreateViewModel
            {
                Id = drink?.DrinkId,
                GlassType = drink?.GlassType,
                Category = drink?.Category,
                Instructions = drink?.Instructions,
                AlcoholicInfo = drink?.AlcoholicInfo,
                Name = drink?.Name,
                Ingredients = drink?.Ingredients,
                ImageUrl = drink?.ImageUrl
            };

            return Ok();
        }

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
        // [HttpGet("Drinks/reviews")]
        // public async Task<IActionResult> ReviewedDrinks(int? pageNumber, [FromQuery] int? pageCount)
        // {
        //     var drinks = _reviewRepository.GetUserReviewedDrinks(_userManager.GetUserId(User));
        //
        //     var model = new DrinksViewModel
        //     {
        //         Drinks = await PaginatedList<Drink>.CreateAsync(drinks, pageNumber ?? 1, pageCount ?? 10)
        //     };
        //     return Ok(model);
        // }

        public async Task<IActionResult> SearchByAlcoholContent([FromQuery] int? pageCount, int? pageNumber, bool alcoholics = true, bool nonAlcoholics = true, bool optionalAlcoholics = true)
        {
            var drinks = _drinkSearchService
                .SearchByAlcoholContent(alcoholics, nonAlcoholics, optionalAlcoholics);

            var model = new DrinksViewModel
            {
                Drinks = await PaginatedList<Drink>.CreateAsync(drinks, pageNumber ?? 1, pageCount ?? 10),
                Alcoholics = alcoholics,
                NonAlcoholics = nonAlcoholics,
                OptionalAlcoholics = optionalAlcoholics
            };
            return Ok(model);
        }

        public async Task<IActionResult> SearchByName(string searchString, int? pageNumber, [FromQuery] int? pageCount)
        {
            var drinks = _drinkRepository.GetAllDrinks();
            if (!string.IsNullOrEmpty(searchString))
            {
                Task.Run(() =>
                    _apiService.CreateUserActivity(PerformedAction.SearchByName, this.User.Identity.Name, searchedPhrase: searchString)).Forget();
                drinks = _drinkSearchService.SearchByName(searchString);
            }

            var model = new DrinksViewModel
            {
                Drinks = await PaginatedList<Drink>.CreateAsync(drinks,
                    pageNumber ?? 1, pageCount ?? 10)
            };
            return Ok(model);
        }

        public async Task<IActionResult> SearchByIngredients(string searchString, int? pageNumber, [FromQuery] int? pageCount,
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


            var model = new DrinksViewModel
            {
                Drinks = await PaginatedList<Drink>.CreateAsync(drinks,
                    pageNumber ?? 1, pageCount ?? 10)
            };
            return Ok(model);
        }
    }
}