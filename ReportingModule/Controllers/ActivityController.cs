using BLL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ReportingModuleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        //POST: https://localhost:5001/api/Activity
        [HttpPost]
        public IActionResult CreateActivity(UserActivityDto model)
        {
            var activity = new UserActivity
            {
                Username = model.Username,
                Action = model.Action,
                Created = DateTime.Now
            };
            // Jakieś reposytorium co dodaje activity do bazy danych
            // this._activityRep.Insert(activity);
            if (string.IsNullOrEmpty(model.Username))
            {
                return BadRequest("Username was empty");
            }
            return Ok();
        }
        [HttpGet]
        public IActionResult GetActivities()
        {
            var activities = new List<UserActivity> { new UserActivity { Username = "Tester" } };
            return Ok(activities);
        }
    }

    public class UserActivityDto
    {
        public string Username { get; set; }
        public string Action { get; set; }
    }

}
