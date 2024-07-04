using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UniTutor.Interface;
using UniTutor.Model;
using UniTutor.Repository;

namespace UniTutor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdmin _admin;
        private IConfiguration _config;
        private readonly IStudent _student;
        private readonly ITutor _tutor;

       
        public AdminController(IAdmin adminRepository, IConfiguration config, IStudent student, ITutor tutor)
        {
            _admin = adminRepository;
            _config = config;
            _student = student;
            _tutor = tutor;
        }


        [HttpPost("create")]
        public IActionResult CreateAdmin([FromBody] Admin admin)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var isAdmin = _admin.IsAdmin(admin);
                if (!isAdmin)
                {
                    var result = _admin.CreateAdmin(admin);
                    if (result)
                    {
                        return Ok();
                    }
                    else
                    {
                        return BadRequest(ModelState);
                    }
                }
                else
                {
                    return NotFound();
                }
            }
        }

        [HttpPost("adminLogin")]
        public IActionResult AdminLogin([FromBody] LoginRequest adminLogin)
        {
            var result = _admin.Login(adminLogin.Email, adminLogin.Password);
            if (result)
            {
                // Retrieve admin details from the database
                var loggedInAdmin = _admin.GetAdminByEmail(adminLogin.Email);

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                new Claim(ClaimTypes.Name, adminLogin.Email),  // Email claim
                new Claim(ClaimTypes.NameIdentifier, loggedInAdmin.Id.ToString()),  // Admin ID claim
                new Claim(ClaimTypes.GivenName, loggedInAdmin.Name),  // Admin name claim
                new Claim(ClaimTypes.Role, "Student")

                    }),
                    Expires = DateTime.UtcNow.AddDays(30),
                    SigningCredentials = credentials
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);

                return Ok(new { token = tokenHandler.WriteToken(token), Id = loggedInAdmin.Id });
            }
            else
            {
                return Unauthorized("Invalid email or password");
            }
        }


        [HttpGet("isAuthenticated")]
        public IActionResult isAthenticated([FromQuery(Name = "token")] string token)
        {
            var validatedToken = _admin.validateToken(token);
            if (validatedToken != null)
            {
                return Ok(new { authenticated = true });
            }
            else
            {
                return Unauthorized(new { authenticated = false });
            }
        }
       
        
        [HttpDelete("delete-student/{id}")]
        public IActionResult DeleteStudent(int id)
        {
            var result = _student.Delete(id);
            if (result)
            {
                return Ok(new { message = "Student deleted successfully." });
            }
            return NotFound(new { message = "Student not found." });
        }

        [HttpDelete("delete-tutor/{id}")]
        public IActionResult DeleteTutor(int id)
        {
            var result = _tutor.Delete(id);
            if (result)
            {
                return Ok(new { message = "Tutor deleted successfully." });
            }
            return NotFound(new { message = "Tutor not found." });
        }

        [HttpGet("AllStudents")]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            var students = _admin.GetAllStudent();
            if (students != null)
            {
                return Ok(students);
            }
            else
            {
                return BadRequest("There is no student");
            }
        }
        [HttpGet("AllTutors")]
        public async Task<ActionResult<IEnumerable<Tutor>>> GetTutors()
        {
            var tutors = _admin.GetAllTutor();
            if (tutors != null)
            {
                return Ok(tutors);
            }
            else
            {
                return BadRequest("There is no student");
            }
        }
        [HttpPut("rejecttutor/{id}")]
        public IActionResult RejectTutor(int id)
        {
            var tutor = _tutor.GetById(id);
           
            if (tutor == null)
            {
                return BadRequest("Tutor object is null");
            }
           var result = _admin.rejectTutors(tutor);
            if (result)
            {
                return Ok("Tutor rejected successfully");
            }
            else
            {
                return StatusCode(500, "An error occurred while rejecting the tutor");
            }
        }
        [HttpPut("accepttutor/{id}")]
        public IActionResult AcceptTutor(int id)
        {
            var tutor = _tutor.GetById(id);
            if (tutor == null)
            {
                return BadRequest("Tutor object is null");
            }
            var result = _admin.acceptTutors(tutor);
            if (result)
            {
                return Ok("Tutor accepted successfully");
            }
            else
            {
                return StatusCode(500, "An error occurred while accepting the tutor");
            }
        }


    }
}