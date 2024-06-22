using System.Security.Claims;
using UniTutor.Model;

namespace UniTutor.Interface
{
    public interface IAdmin
    {
        bool Login(string email, string password);
        bool CreateAdmin(Admin admin);
        public ClaimsPrincipal validateToken(string token);

        public bool IsAdmin(Admin admin);

        public Admin GetAdminByEmail(string Email);
        public Admin GetAdminById(int Id);

        public bool Logout();
        
    }
}
