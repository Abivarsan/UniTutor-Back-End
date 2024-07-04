using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniTutor.Interface;
using static Org.BouncyCastle.Math.EC.ECCurve;
using UniTutor.Model;
using UniTutor.DTO;
using Microsoft.EntityFrameworkCore;
using UniTutor.Repository;

namespace UniTutor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly ISubject _subject;
        private readonly ITutor _tutor;
        private IConfiguration _config;
        public SubjectController(ISubject subject, IConfiguration config, ITutor tutor)
        {
            _subject = subject;
            _config = config;
            _tutor = tutor;
        }
        // POST method to create a new subject
        [HttpPost("createsubject{tutorId}")]
        public async Task<IActionResult> CreateSubject(int tutorId, [FromBody] SubjectRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _subject.CreateSubject(tutorId, request);
            if (!result)
            {
                return NotFound(new { message = "Tutor not found" });
            }

            return CreatedAtAction(nameof(GetSubject), new { tutorId, id = request.title }, request);
        }

        // Example method to get a subject by id and  (not fully implemented)
        [HttpGet("getsubject/{tutorId}/{id}")]
        public async Task<IActionResult> GetSubject(int tutorId, int id)
        {
            var subject =  _subject.GetSubject(tutorId, id);
            if (subject == null)
            {
                return NotFound();
            }
            return Ok(subject);
        }
        [HttpGet("getsubjects/{tutorId}")]
        public async Task<IActionResult> GetSubjectsByTutorId(int tutorId)
        {
            var subjects = await _subject.GetSubjectsByTutorId(tutorId);
            if (subjects == null || !subjects.Any())
            {
                return NotFound(new { message = "No subjects found for this tutor" });
            }
            return Ok(subjects);
        }





    }
    
}
