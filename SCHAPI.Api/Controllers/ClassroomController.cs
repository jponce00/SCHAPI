using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SCHAPI.Application.Dtos.Classroom.Request;
using SCHAPI.Application.Interfaces;
using SCHAPI.Infrastructure.Commons.Bases.Request;

namespace SCHAPI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassroomController : ControllerBase
    {
        private readonly IClassroomApplication _classroomApplication;

        public ClassroomController(IClassroomApplication classroomApplication)
        {
            _classroomApplication = classroomApplication;
        }

        [HttpGet]
        public async Task<IActionResult> ListClassrooms([FromQuery] BaseFiltersRequest filters)
        {
            var response = await _classroomApplication.ListClassrooms(filters);

            return Ok(response);
        }

        [HttpGet("Select")]
        public async Task<IActionResult> ListSelectClassrooms()
        {
            var response = await _classroomApplication.ListSelectClassrooms();

            return Ok(response);
        }

        [HttpGet("{classroomId:int}")]
        public async Task<IActionResult> ClassroomById(int classroomId)
        {
            var response = await _classroomApplication.ClassroomById(classroomId);

            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterClassroom([FromBody] ClassroomRequestDto requestDto)
        {
            var response = await _classroomApplication.RegisterClassroom(requestDto);

            return Ok(response);
        }

        [HttpPut("Edit/{classroomId:int}")]
        public async Task<IActionResult> EditClassroom(int classroomId, [FromBody] ClassroomRequestDto requestDto)
        {
            var response = await _classroomApplication.EditClassroom(classroomId, requestDto);

            return Ok(response);
        }

        [HttpDelete("Remove/{classroomId:int}")]
        public async Task<IActionResult> RemoveClassroom(int classroomId)
        {
            var response = await _classroomApplication.RemoveClassroom(classroomId);

            return Ok(response);
        }
    }
}
