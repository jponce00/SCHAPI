using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SCHAPI.Application.Interfaces;

namespace SCHAPI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleApplication _scheduleApplication;

        public ScheduleController(IScheduleApplication scheduleApplication)
        {
            _scheduleApplication = scheduleApplication;
        }

        [HttpGet("Select")]
        public async Task<IActionResult> ListSelectSchedules()
        {
            var response = await _scheduleApplication.ListSelectSchedules();

            return Ok(response);
        }
    }
}
