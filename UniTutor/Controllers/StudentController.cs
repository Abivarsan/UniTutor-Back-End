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
using UniTutor.DTO;
using UniTutor.Migrations;
using AutoMapper;

namespace UniTutor.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        IStudent _student;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;   
        

        public StudentController(IStudent student,IConfiguration config,IMapper mapper)
        {
            _config = config;
            _student = student;
            _mapper = mapper;
        }
        [HttpPost("create")]
        public IActionResult CreateAccount([FromBody] StudentRegistration studentDto)
        {
            if (ModelState.IsValid)
            {
                var student = _mapper.Map<Student>(studentDto);
                PasswordHash ph = new PasswordHash();
                student.password = ph.HashPassword(studentDto.password); // Hash the password

                var result = _student.SignUp(student);
                if (result)
                {
                    Console.WriteLine("registration success");
                    return CreatedAtAction(nameof(GetAccountById), new { id = student.Id }, student);
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
        [HttpGet("details/{id}")]
        public IActionResult GetAccountById(int id)
        {
            var student = _student.GetById(id);
            if (student == null)
            {
                return NotFound();
            }
            return Ok(student);
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody] Loginrequest studentLogin)
        {
            var email = studentLogin.email;
            var password = studentLogin.password;

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
         new Claim(ClaimTypes.GivenName, loggedInStudent.firstName),  // Student name claim
         new Claim(ClaimTypes.Role, "Student")
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
        [HttpPatch("updatestudent{id}")]
        public async Task<IActionResult> UpdateStudentPartial(int id, [FromBody] UpdateStudent updatedStudent)
        {
            var student = await _student.UpdateStudentProfile(id, updatedStudent);
            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }


    }
}
