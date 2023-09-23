using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SCHAPI.Application.Dtos.Lesson.Request;
using SCHAPI.Application.Interfaces;
using SCHAPI.Infrastructure.Commons.Bases.Request;

namespace SCHAPI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : ControllerBase
    {
        private readonly ILessonApplication _lessonApplication;

        public LessonController(ILessonApplication lessonApplication)
        {
            _lessonApplication = lessonApplication;
        }

        [HttpGet]
        public async Task<IActionResult> ListLessons([FromQuery] BaseFiltersRequest filters)
        {
            var response = await _lessonApplication.ListLessons(filters);

            return Ok(response);
        }

        [HttpGet("{lessonId:int}")]
        public async Task<IActionResult> LessonById(int lessonId)
        {
            var response = await _lessonApplication.LessonById(lessonId);

            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterLesson([FromBody] LessonRequestDto requestDto)
        {
            var response = await _lessonApplication.RegisterLesson(requestDto);

            return Ok(response);
        }

        [HttpPut("Edit/{lessonId:int}")]
        public async Task<IActionResult> EditLesson(int lessonId, [FromBody] LessonRequestDto requestDto)
        {
            var response = await _lessonApplication.EditLesson(lessonId, requestDto);

            return Ok(response);
        }

        [HttpPut("Remove/{lessonId:int}")]
        public async Task<IActionResult> RemoveLesson(int lessonId)
        {
            var response = await _lessonApplication.RemoveLesson(lessonId);

            return Ok(response);
        }
    }
}
