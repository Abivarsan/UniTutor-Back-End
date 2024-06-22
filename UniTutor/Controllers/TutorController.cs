using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UniTutor.Interface;
using UniTutor.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UniTutor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TutorController : ControllerBase
    {
        ITutor _tutor;
        private IConfiguration _config;
        public TutorController(ITutor tutor, IConfiguration config)
        {
            _tutor = tutor;
            _config = config;
        }

       [HttpPost("createAccount")]
        public IActionResult RequestAccount([FromForm] Tutor tutor)
        {
            if (ModelState.IsValid)
            {
                PasswordHash ph = new PasswordHash();
                var Password = ph.HashPassword(tutor.password);
                Console.WriteLine(Password);
                tutor.password = Password;
                Console.WriteLine(tutor.password);

                // Upload CV and Uni_ID
              /* tutor.CV = _tutor.UploadFiles(CV);
                tutor.Uni_ID = _tutor.UploadFile(Uni_ID);*/
               
               var result = _tutor.signUp(tutor);

                if (result)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest("signup failed");
                }
            }
            else
            {
                return BadRequest("Model failed");
            }
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
            new Claim(ClaimTypes.Name, loggedInTutor.Email),  // Email claim
            new Claim(ClaimTypes.NameIdentifier, loggedInTutor.Id.ToString()),  // ID claim
            new Claim(ClaimTypes.GivenName, loggedInTutor.FirstName)  // name claim
                }),
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new { token = tokenHandler.WriteToken(token), Id = loggedInTutor.Id });
        }

        // GET: api/<TutorController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<TutorController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TutorController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<TutorController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TutorController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

