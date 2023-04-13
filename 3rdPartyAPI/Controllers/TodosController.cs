using _3rdPartyAPI.Models;
using _3rdPartyAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace _3rdPartyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly IHolidayApiService _holidayApiService;
        private readonly ITodoApiService _todoApiService;

        public TodosController(
            ITodoApiService todoApiService,
            IHolidayApiService holidayApiService)
        {
            _todoApiService = todoApiService;
            _holidayApiService = holidayApiService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTodoAsync()
        {
            try
            {
                // call holiday API and get the data first
                var holidays = await _holidayApiService.GetHolidays("au", 2021);

                // Map result from holiday to API Todo and post it
                var todosModel = new TodosModel
                {
                    Name = holidays?.FirstOrDefault()?.Name ?? "",
                    IsCompleted = holidays?.FirstOrDefault()?.Global ?? false
                };

                if (todosModel is null)return BadRequest();

                var result = await _todoApiService.PostTodosAsync(todosModel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
