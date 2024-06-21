using Microsoft.AspNetCore.Http;
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
        public IActionResult AdminLogin([FromBody] Admin admin)
        {
            var result = _admin.Login(admin.Email, admin.password);
            if (result)
            {
                // Retrieve admin details from the database
                var loggedInAdmin = _admin.GetAdminByEmail(admin.Email);

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                new Claim(ClaimTypes.Name, admin.Email),  // Email claim
                new(ClaimTypes.NameIdentifier, loggedInAdmin.Id.ToString()),  // Admin ID claim
                new(ClaimTypes.GivenName, loggedInAdmin.Name)  // Admin name claim
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

    }
}