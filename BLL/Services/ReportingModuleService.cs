using BLL.Enums;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ReportingModuleService : IReportingModuleService
    {
        private const string CreateActivityAddress = "https://localhost:5115/api/Activity";
        private const string GetReportAddress = "https://localhost:5115/api/Report/generateReport";
        public async Task CreateUserActivity(PerformedAction action, string? username = null, string drinkId = null, string drinkName = null,
            string searchedPhrase = null, int? score = null)
        {
            using var httpClient = new HttpClient();
            var newUserActivity = new UserActivityDto
            {
                Username = username,
                Action = action,
                DrinkId = drinkId,
                DrinkName = drinkName,
                SearchedPhrase = searchedPhrase,
                Score = score
            };
            var content = new StringContent(JsonConvert.SerializeObject(newUserActivity), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(CreateActivityAddress, content);
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                // Zaloguj mi to moim loggerem
            }
        }

        public async Task<Report> GetReportData(DateTime start, DateTime end)
        {
            using var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(GetReportAddress);
            var content = await response.Content.ReadAsStringAsync();
            var parsedResponse = JsonConvert.DeserializeObject<Report>(content);
            return parsedResponse;
        }
    }
}
