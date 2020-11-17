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
        private readonly IGetReportDataService _reportingService;
        public ReportController(IGetReportDataService reportingService)
        {
            _reportingService = reportingService;
        }
        [HttpGet("get")]
        public async Task<IActionResult> GetReport(DateTime start, DateTime end)
        {
            var report = new ReportDto()
            {
                SuccessfulLoginsAmount = await _reportingService.GetSuccessfulLoginsData(start, end),
                MostFavorableDrink = await _reportingService.GetMostFavorableDrinkData(start, end),
                HighestScoreDrink = await _reportingService.GetHighestScoreDrinkData(start, end),
                LowestScoreDrink = await _reportingService.GetLowestScoreDrinkData(start, end),
                MostVisitedDrink = await _reportingService.GetMostVisitedDrinkData(start, end),
                MostActiveUser = await _reportingService.GetMostActiveUser(start, end),
                MostReviewedDrink = await _reportingService.GetMostReviewedDrinkData(start, end)
            };
            return Ok(report);
        }
    }
}
