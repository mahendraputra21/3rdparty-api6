using _3rdPartyAPI.Models;
using System.Text.Json;
using System.Text;

namespace _3rdPartyAPI.Services
{
    public class TodoApiService : ITodoApiService
    {

        private readonly HttpClient _httpClient;

        public TodoApiService(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient("TodosApi");
        }

        public async Task<TodosModel> PostTodosAsync(TodosModel todos)
        {
            try
            {
                var reqEndpoint = "/todos";
                var json = JsonSerializer.Serialize(todos);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(reqEndpoint, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseJson = await response.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                    var result = JsonSerializer.Deserialize<TodosModel>(responseJson, options) ?? new TodosModel();

                    return result;
                }
                else
                    throw new HttpRequestException("An error has occured");
            }
            catch (Exception ex)
            {
                throw new HttpRequestException(ex.Message);
            }

        }
    }
}
