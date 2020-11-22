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
        
        public ReportController(IReportDataService reportDataService)
        {
            _reportDataService = reportDataService;
        }
        [HttpGet("generateReport")]
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
    }
}
