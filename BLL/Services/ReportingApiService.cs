using BLL.Enums;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ReportingApiService : IReportingApiService
    {
        private const string CreateActivityAddress = "https://localhost:5115/api/Activity";
        public async Task UserDidSomething(PerformedAction action, string? drinkId = null, string? searchedPhrase = null, int? score = null)
        {
            using var httpClient = new HttpClient();
            var newUserActivity = new UserActivityDto
            {
                //Username = this.User.Identity.Name,
                Username = "Czosnek",
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

        //public async Task GetReportData()
        //{
        //    var getResponse = await httpClient.GetAsync(apiAddress);
        //    var getContent = await getResponse.Content.ReadAsStringAsync();
        //    var parsedResponse = JsonConvert.DeserializeObject<List<UserActivity>>(getContent);
        //}
    }
}
