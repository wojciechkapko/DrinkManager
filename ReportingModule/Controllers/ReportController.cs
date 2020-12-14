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
        private readonly IReportDataService _reportDataService;
        
        public ReportController(IReportDataService reportDataService)
        {
            _reportDataService = reportDataService;
        }

        [HttpGet("generateReport/{datesInfo}")]
        public async Task<IActionResult> GenerateNewReport(string datesInfo)
        {
            var bothDates = datesInfo.Split(",");
            var start = DateTime.Parse(bothDates[0]).AddHours(-1);
            var end = DateTime.Parse(bothDates[1]).AddHours(-1);
            var report = new Report()
            {
                StartDate = start,
                EndDate = end,
                NewRegisters = await _reportDataService.GetNewRegisters(start, end),
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
