using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.EntityFrameworkCore;
using UniTutor.DataBase;
using UniTutor.DTO;
using UniTutor.Interface;
using UniTutor.Model;

namespace UniTutor.Repository
{
    public class TutorRepository : ITutor
    {
        private ApplicationDBContext _DBcontext;
        private readonly IConfiguration _config;
       


        public TutorRepository(ApplicationDBContext DBcontext, IConfiguration config)
        {
            _DBcontext = DBcontext;
            _config = config;


        }
        public bool SignUp(Tutor tutor)
        {
            try
            {
                _DBcontext.Tutors.Add(tutor);
                _DBcontext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        
        public bool login(string email, string password)
        {
            var tutor = _DBcontext.Tutors.FirstOrDefault(a => a.universityMail == email);

            if (tutor == null)
            {
                return false;
            }

            PasswordHash ph = new PasswordHash();

            bool isValidPassword = ph.VerifyPassword(password, tutor.password);
            Console.WriteLine($"Password Validation : {isValidPassword}");

            if (isValidPassword)
            {
              
                return true;
            }
            else
            {
                return false;
            }
        }

        public Tutor GetTutorByEmail(string email)
        {
            return _DBcontext.Tutors.FirstOrDefault(x => x.universityMail == email);
        }
        public bool isUser(string email)
        {
            throw new NotImplementedException();
        }

        public bool logout()
        {
            try
            {
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        public bool Delete(int id)
        {
            var tutor = _DBcontext.Tutors.Find(id);
            if (tutor != null)
            {
                _DBcontext.Tutors.Remove(tutor);
                _DBcontext.SaveChanges();
                return true;
            }
            return false;
        }

        public Tutor GetById(int id)
        {
            return _DBcontext.Tutors.Find(id);
        }

        public IEnumerable<Tutor> GetAll()
        {
            return _DBcontext.Tutors.ToList();
        }
        public bool Updatetutor(int id)
        {
            var tutor = _DBcontext.Tutors.Find(id);
            if (tutor != null)
            {
                _DBcontext.Tutors.Update(tutor);
                _DBcontext.SaveChanges();
                return true;

            }
            return false;

        }
        public bool acceptRequest(Request request)
        {
            try
            {
                request.status = 1;
                _DBcontext.Requests.Update(request);
                _DBcontext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        public bool rejectRequest(Request request)
        {
            try
            {
                request.status = -1; 
                _DBcontext.Requests.Update(request);
                _DBcontext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        public ICollection<Request> GetAllRequest(int id)
        {
            var requests = _DBcontext.Requests.Where(r => r.TutorId == id).ToList();
            return requests;
        }
        public ICollection<Request> GetAcceptedRequest(int id)
        {
            var requests = _DBcontext.Requests.Where(r => r.TutorId == id && r.status==1).ToList();
            return requests;
        }
        
        public async Task<Tutor> GetTutorAsync(int id)
        {
            return await _DBcontext.Tutors.FindAsync(id);
        }

        public async Task UpdateTutorAsync(Tutor tutor)
        {
            _DBcontext.Tutors.Update(tutor);
            await _DBcontext.SaveChangesAsync();
        }
        public async Task<Tutor> UpdateTutorProfile(int id, UpdateTutor updatedtutor)
        {
            var tutor = await _DBcontext.Tutors.FindAsync(id);
            if (tutor == null)
            {
                return null;
            }

            if (updatedtutor.firstName != null)
            {
                tutor.firstName = updatedtutor.firstName;
            }
            if (updatedtutor.lastName != null)
            {
                tutor.lastName = updatedtutor.lastName;
            }
            if (updatedtutor.address != null)
            {
                tutor.address = updatedtutor.address;
            }
            //if (updatedtutor.Subject != null)
            //{
            //    tutor.subjects = updatedtutor.Subjects;
            //}
            if (updatedtutor.profileUrl != null)
            {
                tutor.ProfileUrl = updatedtutor.profileUrl;
            }

            _DBcontext.Tutors.Update(tutor);
            await _DBcontext.SaveChangesAsync();

            return tutor;
        }


    }

}
