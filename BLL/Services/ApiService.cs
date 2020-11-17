using BLL.Enums;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ApiService : IApiService
    {
        private const string CreateActivityAddress = "https://localhost:5115/api/Activity";
        private const string GetReportAddress = "https://localhost:5115/api/Report/get";
        public async Task CreateUserActivity(PerformedAction action, string? username, string? drinkId, string? searchedPhrase, int? score)
        {
            using var httpClient = new HttpClient();
            var newUserActivity = new UserActivityDto
            {
                Username = username,
                Action = action,
                DrinkId = drinkId,
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

        public async Task GetReportData()
        {
            using var httpClient = new HttpClient();
            var getResponse = await httpClient.GetAsync(GetReportAddress);
            var getContent = await getResponse.Content.ReadAsStringAsync();
            var parsedResponse = JsonConvert.DeserializeObject<List<UserActivity>>(getContent);
        }
    }
}
