using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SCHAPI.Application.Dtos.Student.Request;
using SCHAPI.Application.Interfaces;
using SCHAPI.Infrastructure.Commons.Bases.Request;

namespace SCHAPI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentApplication _studentApplication;

        public StudentController(IStudentApplication studentApplication)
        {
            _studentApplication = studentApplication;
        }

        [HttpGet]
        public async Task<IActionResult> ListStudents([FromQuery] BaseFiltersRequest filters)
        {
            var response = await _studentApplication.ListStudents(filters);

            return Ok(response);
        }

        [HttpGet("{studentId:int}")]
        public async Task<IActionResult> StudentById(int studentId)
        {
            var response = await _studentApplication.StudentById(studentId);

            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterStudent([FromBody] StudentRequestDto requestDto)
        {
            var response = await _studentApplication.RegisterStudent(requestDto);

            return Ok(response);
        }

        [HttpPut("Edit/{studentId:int}")]
        public async Task<IActionResult> EditStudent(int studentId, [FromBody] StudentRequestDto requestDto)
        {
            var response = await _studentApplication.EditStudent(studentId, requestDto);

            return Ok(response);
        }

        [HttpDelete("Remove/{studentId:int}")]
        public async Task<IActionResult> RemoveStudent(int studentId)
        {
            var response = await _studentApplication.RemoveStudent(studentId);

            return Ok(response);
        }
    }
}
