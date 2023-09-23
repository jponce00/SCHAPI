using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SCHAPI.Application.Dtos.Subject.Request;
using SCHAPI.Application.Interfaces;
using SCHAPI.Infrastructure.Commons.Bases.Request;

namespace SCHAPI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectApplication _subjectApplication;

        public SubjectController(ISubjectApplication subjectApplication)
        {
            _subjectApplication = subjectApplication;
        }

        [HttpGet]
        public async Task<IActionResult> ListSubjects([FromQuery] BaseFiltersRequest filters)
        {
            var response = await _subjectApplication.ListSubjects(filters);

            return Ok(response);
        }

        [HttpGet("Select")]
        public async Task<IActionResult> ListSelectSubjects()
        {
            var response = await _subjectApplication.ListSelectSubjects();

            return Ok(response);
        }

        [HttpGet("{subjectId:int}")]
        public async Task<IActionResult> SubjectById(int subjectId)
        {
            var response = await _subjectApplication.SubjectById(subjectId);

            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterSubject([FromBody] SubjectRequestDto requestDto)
        {
            var response = await _subjectApplication.RegisterSubject(requestDto);

            return Ok(response);
        }

        [HttpPut("Edit/{subjectId:int}")]
        public async Task<IActionResult> EditSubject(int subjectId, [FromBody] SubjectRequestDto requestDto)
        {
            var response = await _subjectApplication.EditSubject(subjectId, requestDto);

            return Ok(response);
        }

        [HttpPut("Remove/{subjectId:int}")]
        public async Task<IActionResult> RemoveSubject(int subjectId)
        {
            var response = await _subjectApplication.RemoveSubject(subjectId);

            return Ok(response);
        }
    }
}
