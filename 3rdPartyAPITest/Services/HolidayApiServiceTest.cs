using _3rdPartyAPI.Services;
using Microsoft.Extensions.DependencyInjection;

namespace _3rdPartyAPITest.Services
{
    public class HolidayApiServiceTest
    {
        private readonly IHolidayApiService holidayApiService;

        public HolidayApiServiceTest()
        {
            var services = new ServiceCollection();
            services.AddHttpClient();
            services.AddSingleton<IHolidayApiService, HolidayApiService>();
            var serviceProvider = services.BuildServiceProvider();
            var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();
            
            holidayApiService = new HolidayApiService(httpClientFactory);
        }

        [Fact]
        public async Task GetHolidays_ReturnsSuccessStatusCode()
        {
            // Arrange
            var countryCode = "au";
            var year = 2021;

            // Act
            var response = await holidayApiService.GetHttpResponse(countryCode, year);

            // Assert
            Assert.NotNull(response);
            response.EnsureSuccessStatusCode();
        }
    }

}
