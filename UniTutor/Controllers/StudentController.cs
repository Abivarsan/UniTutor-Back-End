using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore;
using System.Threading.Tasks;
using UniTutor.DataBase;
using UniTutor.Model;
using UniTutor.Interface;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using UniTutor.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using UniTutor.Services;

namespace UniTutor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        IStudent _student;
        private readonly IConfiguration _config;
        

        public StudentController(IStudent student,IConfiguration config)
        {
            _config = config;
            _student = student;
            
        }
           [HttpPost]
           [Route("create")]
           public IActionResult CreateAccount([FromBody]Student student)
           {
                      
            if (ModelState.IsValid)
            {

                var result = _student.SignUp(student);
                if (result)
                {
                    Console.WriteLine("registration success");
                    return Ok(result);    

                }
                else
                {
                    Console.WriteLine("registration failed");
                    return BadRequest(result);
                }
            }
            else
            {
                return BadRequest("ModelError");
            }

        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest studentLogin)
        {
            var email = studentLogin.Email;
            var password = studentLogin.Password;

            var result = _student.Login(email, password);
            if (result)
            {
                var loggedInStudent = _student.GetByMail(email);

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
         new Claim(ClaimTypes.Name, email),  // Email claim
         new Claim(ClaimTypes.NameIdentifier, loggedInStudent.Id.ToString()),  // Student ID claim
         new Claim(ClaimTypes.GivenName, loggedInStudent.FirstName)  // Student name claim
                    }),
                    Expires = DateTime.UtcNow.AddDays(30),
                    SigningCredentials = credentials
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var token = tokenHandler.CreateToken(tokenDescriptor);

                return Ok(new { token = tokenHandler.WriteToken(token), Id = loggedInStudent.Id });
            }
            else
            {
                return Unauthorized("Invalid email or password");
            }
        }
        [HttpGet("details")]
        [Authorize]
        public IActionResult GetStudentDetails()
        {
            // Extract student ID from the token claims
            var studentIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (studentIdClaim == null)
            {
                return Unauthorized("Student ID not found in token");
            }

            var studentId = int.Parse(studentIdClaim.Value);

            // Fetch student details from the database
            var student = _student.GetById(studentId);
            if (student == null)
            {
                return NotFound("Student not found");
            }

            // Return the student details
            return Ok(student);
        }
    
    

   

    [HttpPost("requesttutor")]
        public IActionResult requesttutor([FromBody] Request request)
        {
            var result = _student.CreateRequest(request);
            if (result)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpDelete("deleterequest")]
        public IActionResult deleterequest([FromBody] Request request)
        {
            var result = _student.DeleteRequest(request);
            if (result)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest();
            }
        }

    }
}
