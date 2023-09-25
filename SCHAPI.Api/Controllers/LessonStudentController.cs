using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SCHAPI.Application.Dtos.LessonStudent.Request;
using SCHAPI.Application.Interfaces;
using SCHAPI.Infrastructure.Commons.Bases.Request;

namespace SCHAPI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonStudentController : ControllerBase
    {
        private readonly ILessonStudentApplication _lessonStudentApplication;

        public LessonStudentController(ILessonStudentApplication lessonStudentApplication)
        {
            _lessonStudentApplication = lessonStudentApplication;
        }

        [HttpGet]
        public async Task<IActionResult> ListLessonStudents([FromQuery] BaseFiltersRequest filters)
        {
            var response = await _lessonStudentApplication.ListLessonStudents(filters);

            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterLessonStudent([FromBody] LessonStudentRequestDto requestDto)
        {
            var response = await _lessonStudentApplication.RegisterLessonStudent(requestDto);

            return Ok(response);
        }

        [HttpDelete("Remove/{lessonStudentId:int}")]
        public async Task<IActionResult> RemoveLessonStudent(int lessonStudentId)
        {
            var response = await _lessonStudentApplication.RemoveLessonStudents(lessonStudentId);

            return Ok(response);
        }
    }
}
