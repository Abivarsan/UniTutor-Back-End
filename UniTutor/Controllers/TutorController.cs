using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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
        public TutorController(ITutor tutor, IConfiguration config)
        {
            _tutor = tutor;
            _config = config;
        }

        /*[HttpPost("createAccount")]
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

        /* var result = _tutor.signUp(tutor);

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
  }*/
        /*  [HttpPost("createAccount")]
          public async Task<IActionResult> RequestAccount([FromForm] Tutor tutor, [FromForm] IFormFile CvFile, [FromForm] IFormFile UniIdFile)
          {
              if (ModelState.IsValid)
              {
                  PasswordHash ph = new PasswordHash();
                  var hashedPassword = ph.HashPassword(tutor.password);
                  tutor.password = hashedPassword;

                  tutor.CvFile = CvFile;
                  tutor.UniIdFile = UniIdFile;

                  var result = _tutor.SignUp(tutor);

                  if (result)
                  {
                      return Ok(new { message = "Tutor signed up successfully." });
                  }
                  else
                  {
                      return BadRequest("Signup failed.");
                  }
              }
              else
              {
                  return BadRequest("Invalid model state.");
              }
          }*/



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
        [HttpPost("create")]
        public async Task<IActionResult> Upload([FromForm] TutorViewModel model)
        {
            if ((model.CvFile == null || model.CvFile.Length == 0) || (model.UniIdFile == null || model.UniIdFile.Length == 0))
                return BadRequest("No file uploaded.");

            try
            {
                var tutorId = await _tutor.AddTutorWithFilesAsync(model);
                return Ok(new { Id = tutorId });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("CvFile/{id}")]
        public async Task<IActionResult> GetImage(int id)
        {
            try
            {
                var tutor = await _tutor.GetCvFileAsync(id);
                if (tutor == null)
                    return NotFound();

                return File(tutor.CVData, tutor.CVContentType, tutor.CVFileName);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("UniID/{id}")]
        public async Task<IActionResult> GetUniID(int id)
        {
            try
            {
                var tutor = await _tutor.GetUniIDAsync(id);
                if (tutor == null)
                    return NotFound();

                return File(tutor.UniIDData, tutor.UniIDContentType, tutor.UniIDFileName);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTutor(int id)
        {
            var tutor = await _tutor.GetTutorAsync(id);
            if (tutor == null)
                return NotFound();

            return Ok(tutor);
        }
        


    }
}

