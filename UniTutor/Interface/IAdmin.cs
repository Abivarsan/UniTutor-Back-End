using System.Security.Claims;
using UniTutor.Model;

namespace UniTutor.Interface
{
    public interface IAdmin
    {
        
        
        public ClaimsPrincipal validateToken(string token);

        public bool IsAdmin(Admin admin);

        public Admin GetAdminByEmail(string Email);
        public Admin GetAdminById(int Id);

        public bool Logout();
        bool Login(string email, string password);
        bool CreateAdmin(Admin admin);

        public IEnumerable<Student> GetAllStudent();
        public IEnumerable<Tutor> GetAllTutor();
        public bool acceptTutors(Tutor tutor);
        public bool rejectTutors(Tutor tutor);



    }
}
