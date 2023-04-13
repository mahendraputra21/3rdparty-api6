using _3rdPartyAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace _3rdPartyAPI.Services
{
    public interface ITodoApiService
    {
        Task<TodosModel> PostTodosAsync(TodosModel todos);
    }
}
