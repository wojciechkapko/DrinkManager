using BLL.Enums;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ReportingApiService : IReportingApiService
    {
        public async Task UserDidSomething(string action)
        {
            using var httpClient = new HttpClient();
            var apiAddress = "https://localhost:5115/api/Activity";
            var newUserActivity = new UserActivityDto
            {
                //Username = this.User.Identity.Name,
                Username = "Czosnek",
                Action = action
            };
            var content = new StringContent(JsonConvert.SerializeObject(newUserActivity), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(apiAddress, content);
            if (response.StatusCode == HttpStatusCode.BadRequest)
            {
                // Zaloguj mi to moim loggerem
            }
            var getResponse = await httpClient.GetAsync(apiAddress);
            var getContent = await getResponse.Content.ReadAsStringAsync();
            var parsedResponse = JsonConvert.DeserializeObject<List<UserActivity>>(getContent);
        }
    }
}
