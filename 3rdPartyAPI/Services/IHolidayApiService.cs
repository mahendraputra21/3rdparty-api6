using _3rdPartyAPI.Models;

namespace _3rdPartyAPI.Services
{
    public interface IHolidayApiService
    {
        Task<List<HolidayModel>> GetHolidays(string countryCode, int year);
        Task<HttpResponseMessage> GetHttpResponse(string countryCode, int year);
    }
}
