using BLL;
using BLL.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ReportingModuleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly DateTime start = new DateTime(2020,10,10);
        private readonly DateTime end = DateTime.Now;
        private readonly IReportDataService _reportDataService;
        private readonly IUserOrientedDataService _userOrientedDataService;
        
        public ReportController(IReportDataService reportDataService, IUserOrientedDataService userOrientedDataService)
        {
            _reportDataService = reportDataService;
            _userOrientedDataService = userOrientedDataService;
        }

        //TODO: Remove constants, add to Wojtek's view with Report Settings manual generation of Report 
        [HttpGet("generateGeneralReport")]
        public async Task<IActionResult> GenerateNewReport(/*DateTime start, DateTime end*/)
        {
            var report = new Report()
            {
                StartDate = start,
                EndDate = end,
                SuccessfulLoginsAmount = await _reportDataService.GetSuccessfulLoginsData(start, end),
                MostFavouriteDrink = await _reportDataService.GetMostFavouriteDrinkData(start, end),
                HighestScoreDrink = await _reportDataService.GetHighestScoreDrinkData(start, end),
                LowestScoreDrink = await _reportDataService.GetLowestScoreDrinkData(start, end),
                MostVisitedDrink = await _reportDataService.GetMostVisitedDrinkData(start, end),
                MostActiveUser = await _reportDataService.GetMostActiveUser(start, end),
                MostReviewedDrink = await _reportDataService.GetMostReviewedDrinkData(start, end)
            };

            return Ok(report);
        }

        [HttpGet("checkUser")]
        public async Task<IActionResult> GenerateUserReport(string username)
        {
            var userReport = new UserReport()
            {
                Username = username,
                RegisteredAt = await _userOrientedDataService.GetUserCreationDate(username),
                LoginsCount = await _userOrientedDataService.GetUserLoginsCount(username),
                LastSeen = await _userOrientedDataService.GetTimeSinceLastUserActivity(username),
                MostVisitedDrink = await _userOrientedDataService.GetMostVisitedDrinkData(username),
                RecentlyFavouriteDrink = await _userOrientedDataService.GetLastAddedFavouriteDrink(username),
                RecentlyReviewedDrink = await _userOrientedDataService.GetLastReviewedDrink(username)
            };

            return Ok(userReport);
        }
    }
}
