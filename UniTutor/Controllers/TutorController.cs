using AutoMapper;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UniTutor.DTO;
using UniTutor.Interface;
using UniTutor.Model;
using UniTutor.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UniTutor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TutorController : ControllerBase
    {
        ITutor _tutor;
        private IConfiguration _config;
        private readonly IMapper _mapper;
        public TutorController(ITutor tutor, IConfiguration config,IMapper mapper)
        {
            _tutor = tutor;
            _config = config;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public IActionResult CreateAccount([FromBody] TutorRegistration tutorDto)
        {
            if (ModelState.IsValid)
            {
                var tutor = _mapper.Map<Tutor>(tutorDto);
                PasswordHash ph = new PasswordHash();
                tutor.password = ph.HashPassword(tutorDto.password); // Hash the password

                var result = _tutor.SignUp(tutor);
                if (result)
                {
                    Console.WriteLine("registration success");
                    return CreatedAtAction(nameof(GetAccountById), new { id = tutor.Id }, tutor);
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
            var tutor = _tutor.GetById(id);
            if (tutor == null)
            {
                return NotFound();
            }
            return Ok(tutor);
        }


        [HttpPost("login")]
        public IActionResult login([FromBody] LoginRequest tutor)
        {
            var email = tutor.Email;
            var password = tutor.Password;

            var result = _tutor.login(email, password);

            if (!result)
            {
                return Unauthorized($"Username Password Incorrect {result}");
            }

            var loggedInTutor = _tutor.GetTutorByEmail(email);

            // Authentication successful, generate JWT token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.Name, loggedInTutor.universityMail),  // Email claim
            new Claim(ClaimTypes.NameIdentifier, loggedInTutor.Id.ToString()),  // ID claim
            new Claim(ClaimTypes.GivenName, loggedInTutor.firstName),  // name claim
            new Claim(ClaimTypes.Role, "Tutor")
                }),
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new { token = tokenHandler.WriteToken(token), Id = loggedInTutor.Id });
        }

        



        [HttpGet("{id}")]
        public async Task<IActionResult> GetTutor(int id)
        {
            var tutor = await _tutor.GetTutorAsync(id);
            if (tutor == null)
                return NotFound();

            return Ok(tutor);
        }
        [HttpPatch("acceptRequest")]
        public IActionResult acceptrequest(Request request)
        {

            var result = _tutor.acceptRequest(request);
            if (result)
            {
                return Ok("Request Accepted");
            }
            else
            {
                return BadRequest("Request Accept failed");
            }


        }

        [HttpPatch("rejectRequest")]
        public IActionResult rejectProject(Request request)
        {
            var result = _tutor.rejectRequest(request);
            if (result)
            {
                return Ok("Project Rejected");
            }
            else
            {
                return BadRequest("Project reject failed");
            }
        }

        [HttpGet("getallrequests")]
        public IActionResult getRequest([FromQuery(Name = "id")] int Id)
        {
            var requests = _tutor.GetAllRequest(Id);
            if (requests != null)
            {
                return Ok(requests);
            }
            else
            {
                return BadRequest("There is no request");
            }
        }
        [HttpGet("getacceptrequests")]
        public IActionResult getAcceptRequest([FromQuery(Name = "id")] int Id)
        {
            var requests = _tutor.GetAcceptedRequest(Id);
            if (requests != null)
            {
                return Ok(requests);
            }
            else
            {
                return BadRequest("There is no accept request");
            }
        }
       
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateTutor(int id, [FromBody] UpdateTutor updatedtutor)
        {
            var tutor = await _tutor.UpdateTutorProfile(id, updatedtutor);
            if (tutor == null)
            {
                return NotFound();
            }

            return Ok(tutor);
        }



    }
}

