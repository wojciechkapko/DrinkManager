using BLL;
using BLL.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ReportingModuleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly IActivitiesRepository _activitiesRepository;
        public ActivityController(IActivitiesRepository activitiesRepository)
        {
            _activitiesRepository = activitiesRepository;
        }
        //POST: https://localhost:5115/api/Activity
        [HttpPost]
        public async Task<IActionResult> CreateActivity(UserActivityDto model)
        {
            var activity = new UserActivity
            {
                Username = model.Username,
                Action = model.Action,
                Created = DateTime.Now
            };

            //var drinksAddedToFavourite = await _activitiesRepository.Get(x =>
            //    x.Action == PerformedAction.AddedToFavourite && !string.IsNullOrEmpty(x.DrinkId))
            //    .GroupBy(y);
            // Id == drinkId
            //var mostFavorableDrink = list.GroupBy(x => x.Id).Select(x => new
            //{
            //    Id = x.Key,
            //    Count = x.Count()
            //}).OrderByDescending(x => x.Count).First();

            await _activitiesRepository.AddActivity(activity);
            await _activitiesRepository.SaveChanges();
            if (string.IsNullOrEmpty(model.Username))
            {
                return BadRequest("Username was empty");
            }
            return Ok();
        }
        //[HttpGet]
        //public IActionResult GetActivities()
        //{
        //    var activities = new List<UserActivity> { new UserActivity { Username = "Tester" } };
        //    return Ok(activities);
        //}
    }
}
