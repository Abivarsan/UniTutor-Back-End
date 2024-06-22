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

        public bool signUp(Tutor tutor)
        {
            try
            {
                _DBcontext.Tutors.Add(tutor);
                _DBcontext.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
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

        /*public string UploadFile(IFormFile file)
        {
            try
            {
                // Check if the file exists
                if (file == null || file.Length == 0)
                {
                    throw new ArgumentNullException(nameof(file), "No file uploaded");
                }

                // Upload file to Cloudinary
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, file.OpenReadStream())
                };

                var uploadResult = _cloudinary.Upload(uploadParams);

                // Return the URL of the uploaded file
                return uploadResult.Uri.AbsoluteUri;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }

        }
        public string[] UploadFiles(IFormFile[] files)
        {
            try
            {
                if (files == null || files.Length == 0)
                {
                    throw new ArgumentNullException(nameof(files), "No files uploaded");
                }

                var uploadedUrls = new List<string>();

                foreach (var file in files)
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(file.FileName, file.OpenReadStream())
                    };

                    var uploadResult = _cloudinary.Upload(uploadParams);
                    uploadedUrls.Add(uploadResult.Uri.AbsoluteUri);
                }

                return uploadedUrls.ToArray();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }

        }*/
    }

}
