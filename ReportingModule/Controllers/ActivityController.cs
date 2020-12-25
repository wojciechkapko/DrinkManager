using BLL;
using Microsoft.AspNetCore.Mvc;
using ReportingModuleApi.Data.Repositories;
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
                DrinkId = model.DrinkId,
                DrinkName = model.DrinkName,
                SearchedPhrase = model.SearchedPhrase,
                Score = model.Score,
                Created = DateTime.Now
            };
            await _activitiesRepository.AddActivity(activity);
            await _activitiesRepository.SaveChanges();
            if (string.IsNullOrEmpty(model.Username))
            {
                return BadRequest("Username was empty");
            }
            return Ok();
        }
    }
}
