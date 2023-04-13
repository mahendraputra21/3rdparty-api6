using _3rdPartyAPI.Models;
using System.Text.Json;

namespace _3rdPartyAPI.Services
{
    public class HolidayApiService : IHolidayApiService
    {
        private readonly HttpClient client;

        public HolidayApiService(IHttpClientFactory clientFactory)
        {
            client = clientFactory.CreateClient("PublicHolidaysApi");
            client.BaseAddress = new Uri("https://date.nager.at/");

        }

        public async Task<List<HolidayModel>> GetHolidays(string countryCode, int year)
        {
            var url = $"/api/v2/PublicHolidays/{year}/{countryCode}";
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var holidays = JsonSerializer.Deserialize<List<HolidayModel>>(json, options) ?? new List<HolidayModel>();

            return holidays;
        }

        public async Task<HttpResponseMessage> GetHttpResponse(string countryCode, int year)
        {
            var url = $"/api/v2/PublicHolidays/{year}/{countryCode}";
            var response = await client.GetAsync(url);

            return response;
        }
    }
}
