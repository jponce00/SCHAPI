using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SCHAPI.Application.Dtos.Teacher.Request;
using SCHAPI.Application.Interfaces;
using SCHAPI.Infrastructure.Commons.Bases.Request;

namespace SCHAPI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherApplication _teacherApplication;

        public TeacherController(ITeacherApplication teacherApplication)
        {
            _teacherApplication = teacherApplication;
        }

        [HttpGet]
        public async Task<IActionResult> ListTeachers([FromQuery] BaseFiltersRequest filters)
        {
            var response = await _teacherApplication.ListTeachers(filters);

            return Ok(response);
        }

        [HttpGet("Select")]
        public async Task<IActionResult> ListSelectTeachers()
        {
            var response = await _teacherApplication.ListSelectTeachers();

            return Ok(response);
        }

        [HttpGet("{teacherId:int}")]
        public async Task<IActionResult> TeacherById(int teacherId)
        {
            var response = await _teacherApplication.TeacherById(teacherId);

            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterTeacher([FromBody] TeacherRequestDto requestDto)
        {
            var response = await _teacherApplication.RegisterTeacher(requestDto);

            return Ok(response);
        }

        [HttpPut("Edit/{teacherId:int}")]
        public async Task<IActionResult> EditTeacher(int teacherId, [FromBody] TeacherRequestDto requestDto)
        {
            var response = await _teacherApplication.EditTeacher(teacherId, requestDto);

            return Ok(response);
        }

        [HttpDelete("Remove/{teacherId:int}")]
        public async Task<IActionResult> RemoveTeacher(int teacherId)
        {
            var response = await _teacherApplication.RemoveTeacher(teacherId);

            return Ok(response);
        }
    }
}
