using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.EntityFrameworkCore;
using UniTutor.DataBase;
using UniTutor.Interface;
using UniTutor.Model;

namespace UniTutor.Repository
{
    public class TutorRepository : ITutor
    {
        private ApplicationDBContext _DBcontext;
        private readonly IConfiguration _config;
        private readonly Cloudinary _cloudinary;


        public TutorRepository(ApplicationDBContext DBcontext, Cloudinary cloudinary)
        {
            _DBcontext = DBcontext;
            _cloudinary = cloudinary;
        }

      /*  public bool SignUp(Tutor tutor)
        {
            if (tutor.CvFile != null)
            {
                using (var ms = new MemoryStream())
                {
                    tutor.CvFile.CopyTo(ms);
                    tutor.CVFileName = tutor.CvFile.FileName;
                    tutor.CVContentType = tutor.CvFile.ContentType;
                    tutor.CVData = ms.ToArray();
                }
            }

            if (tutor.UniIdFile != null)
            {
                using (var ms = new MemoryStream())
                {
                    tutor.UniIdFile.CopyTo(ms);
                    tutor.UniIDFileName = tutor.UniIdFile.FileName;
                    tutor.UniIDContentType = tutor.UniIdFile.ContentType;
                    tutor.UniIDData = ms.ToArray();
                }
            }

            _DBcontext.Tutors.Add(tutor);
            return _DBcontext.SaveChanges() > 0;
        }*/
        
        public bool login(string email, string password)
        {
            var tutor = _DBcontext.Tutors.FirstOrDefault(a => a.Email == email);

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
            return _DBcontext.Tutors.FirstOrDefault(x => x.Email == email);
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
                _DBcontext.Request.Update(request);
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
                _DBcontext.Request.Update(request);
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
            var requests = _DBcontext.Request.Where(r => r.TutorId == id).ToList();
            return requests;
        }
        public ICollection<Request> GetAcceptedRequest(int id)
        {
            var requests = _DBcontext.Request.Where(r => r.TutorId == id && r.status==1).ToList();
            return requests;
        }
        public async Task<int> AddTutorWithFilesAsync(TutorViewModel model)
        {
            if ((model.CvFile == null || model.CvFile.Length == 0) || (model.UniIdFile == null || model.UniIdFile.Length == 0))
                throw new ArgumentException("No file uploaded.");

            using (var imageStream = new MemoryStream())
            using (var pdfStream = new MemoryStream())
            {
                await model.CvFile.CopyToAsync(imageStream);
                await model.UniIdFile.CopyToAsync(pdfStream);

                var tutor = new Tutor
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    password = model.password,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber,
                    Ocupation = model.Ocupation,
                    ModeOfTeaching = model.ModeOfTeaching,
                    Medium = model.Medium,
                    subject = model.subject,
                    Qualification = model.Qualification,
                    HomeTown = model.HomeTown,
                    CVFileName = model.CvFile.FileName,
                    CVContentType = model.CvFile.ContentType,
                    CVData = imageStream.ToArray(),
                    UniIDFileName = model.UniIdFile.FileName,
                    UniIDContentType = model.UniIdFile.ContentType,
                    UniIDData = pdfStream.ToArray()
                };

                _DBcontext.Tutors.Add(tutor);
                await _DBcontext.SaveChangesAsync();
                return tutor.Id;
            }
        }

        public async Task<Tutor> GetUniIDAsync(int id)
        {
            var tutor = await _DBcontext.Tutors.FindAsync(id);
            if (tutor == null)
                throw new ArgumentException("Tutor not found.");

            return tutor;
        }

        public async Task<Tutor> GetCvFileAsync(int id)
        {
            var tutor = await _DBcontext.Tutors.FindAsync(id);
            if (tutor == null)
                throw new ArgumentException("Tutor not found.");

            return tutor;
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


    }

}
