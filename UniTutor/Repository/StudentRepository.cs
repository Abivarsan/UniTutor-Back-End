using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniTutor.DataBase;
using UniTutor.Interface;
using UniTutor.Model;

namespace UniTutor.Repository
{
    public class StudentRepository : IStudent
    {
        private ApplicationDBContext _DBcontext;
        private readonly IConfiguration _config;
        private readonly Cloudinary _cloudinary;
        public StudentRepository(ApplicationDBContext DBcontext , IConfiguration config)
        {
            _DBcontext = DBcontext;
            
        }
        public bool SignUp(Student student)
        {
            try
            {
              PasswordHash ph = new PasswordHash();
              student.Password = ph.HashPassword(student.Password);
                _DBcontext.Students.Add(student);
                _DBcontext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
        public bool Login(string email, string password)
        {
            var student = _DBcontext.Students.FirstOrDefault(c => c.Email == email);

            if (student == null)
            {
                return false;
            }

           PasswordHash ph = new PasswordHash();

           bool isValidPassword = ph.VerifyPassword(password, student.Password);

            if (isValidPassword)
            {
                return true;
            }
            else
            {
                return false;
            }


        }
        public Student GetByMail(string Email)
        {
            return _DBcontext.Students.FirstOrDefault(s => s.Email == Email);
        }
        public bool Delete(int id)
        {
            var student = _DBcontext.Students.Find(id);
            if (student != null)
            {
                _DBcontext.Students.Remove(student);
                _DBcontext.SaveChanges();
                return true;
            }
            return false;
        }
        public Student GetById(int id)
        {
            return _DBcontext.Students.Find(id);
        }

        public IEnumerable<Student> GetAll()
        {
            return _DBcontext.Students.ToList();
        }

       /* public string UploadFile(IFormFile file)
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

        }*/
        public bool UpdateStudent(Student student)
        // public bool UpdateStudent(Student student, IFormFile ProfileImage)
        {
            var existingstudent = _DBcontext.Students.Find(student.Id);
            if (existingstudent != null)
            {
                existingstudent.FirstName = student.FirstName;
                existingstudent.LastName = student.LastName;
                existingstudent.Grade = student.Grade;
                existingstudent.Address = student.Address;
                existingstudent.HomeTown = student.HomeTown;
                existingstudent.PhoneNumber = student.PhoneNumber;
                //update the student profile
              /*  if (ProfileImage != null)
                {
                    var uploadResult = new ImageUploadResult();

                    using (var stream = ProfileImage.OpenReadStream())
                    {
                        var uploadParams = new ImageUploadParams()
                        {
                            File = new FileDescription(ProfileImage.FileName, stream)
                        };
                        uploadResult = _cloudinary.Upload(uploadParams);
                    }
                    existingstudent.ProfileImage = uploadResult.Url.ToString();
                }*/
                    _DBcontext.Students.Update(existingstudent);
                _DBcontext.SaveChanges();
                return true;
                 
            }
            return false;
            
        }
       
    }
}
