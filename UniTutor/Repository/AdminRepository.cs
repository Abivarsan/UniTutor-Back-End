
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UniTutor.DataBase;
using UniTutor.Interface;
using UniTutor.Model;

namespace UniTutor.Repository
{
    public class AdminRepository:IAdmin
    {
        private ApplicationDBContext _DBcontext;
        private readonly IConfiguration _config;
        
        public AdminRepository(ApplicationDBContext DBcontext, IConfiguration config)
        {
            _DBcontext = DBcontext;
            _config = config;
           
        }

        public bool CreateAdmin(Admin admin)
        {
            try
            {
                PasswordHash ph = new PasswordHash();
                admin.password = ph.HashPassword(admin.password);
                _DBcontext.Admin.Add(admin);
                _DBcontext.SaveChanges(true);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Login(string email, string password)
        {
            var admin = _DBcontext.Admin.FirstOrDefault(a => a.Email == email);

            if (admin == null)
            {
                return false;
            }

            PasswordHash ph = new PasswordHash();

            bool isValidPassword = ph.VerifyPassword(password, admin.password);

            if (isValidPassword)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsAdmin(Admin admin)
        {
            return _DBcontext.Admin.Any(a => a.Email == admin.Email);
        }
        public ClaimsPrincipal validateToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                if (validatedToken != null)
                {
                    return new ClaimsPrincipal(new ClaimsIdentity(((JwtSecurityToken)validatedToken).Claims));
                }

                return null;
            }
            catch
            {
                return null;
            }
        }
        public Admin GetAdminByEmail(string Email)
        {
            return _DBcontext.Admin.FirstOrDefault(x => x.Email == Email);
        }

        public Admin GetAdminById(int Id)
        {
            return _DBcontext.Admin.FirstOrDefault(x => x.Id == Id);
        }
        public IEnumerable<Student> GetAllStudent()
        {
            return _DBcontext.Students.ToList();
        }
        public IEnumerable<Tutor> GetAllTutor()
        {
            return _DBcontext.Tutors.ToList();
        }
        public bool acceptTutors(Tutor tutor)
        {
            try
            {
                tutor.accept = 1;
                _DBcontext.Tutors.Update(tutor);
                _DBcontext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        public bool rejectTutors(Tutor tutor)
        {
            try
            {
                tutor.accept = -1;
                _DBcontext.Tutors.Update(tutor);
                _DBcontext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString()); ;
                return false;
            }
        }
        public bool Logout()
        {
            throw new NotImplementedException();
        }
    }
}
