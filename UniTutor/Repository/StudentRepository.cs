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
              student.password = ph.HashPassword(student.password);
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
            try
            {
                var student = _DBcontext.Students.FirstOrDefault(a => a.Email == email);

                if (student == null)
                {
                    Console.WriteLine("Student not found.");
                    return false;
                }

                PasswordHash ph = new PasswordHash();

                bool isValidPassword = ph.VerifyPassword(password, student.password);

                return isValidPassword;
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine($"InvalidCastException: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General exception: {ex.Message}");
                throw;
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

       

        
        public bool SignOut()
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
    
        public bool CreateRequest(Request request)
        {
            try
            {
                _DBcontext.Request.Add(request);
                _DBcontext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool DeleteRequest(Request request) 
        {
            try
            {
                _DBcontext.Request.Remove(request);
                _DBcontext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

       

        public async Task<bool> Update(Student student)
        {
            _DBcontext.Students.Update(student);
            return await _DBcontext.SaveChangesAsync() > 0;
        }
        //abivarsan anna 
        public async Task<int> AddStudentWithImageAsync(StudentViewModel model)
        {
            if (model.Image == null || model.Image.Length == 0)
                throw new ArgumentException("No file uploaded.");

            using (var ms = new MemoryStream())
            {
                await model.Image.CopyToAsync(ms);
                var student = new Student
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Grade = model.Grade,
                    Address = model.Address,
                    HomeTown = model.HomeTown,
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email,
                    password = model.password,
                    VerificationCode = model.VerificationCode,
                    FileName = model.Image.FileName,
                    ContentType = model.Image.ContentType,
                    Data = ms.ToArray()
                };

                _DBcontext.Students.Add(student);
                await _DBcontext.SaveChangesAsync();
                return student.Id;
            }
        }

        public async Task<Student> GetImageAsync(int id)
        {
            var student = await _DBcontext.Students.FindAsync(id);
            if (student == null)
                throw new ArgumentException("Student not found.");

            return student;
        }

        public async Task<Student> GetStudentAsync(int id)
        {
            return await _DBcontext.Students.FindAsync(id);
        }

        public async Task AddStudentAsync(Student student)
        {
            _DBcontext.Students.Add(student);
            await _DBcontext.SaveChangesAsync();
        }

        public async Task UpdateStudentAsync(Student student)
        {
            _DBcontext.Students.Update(student);
            await _DBcontext.SaveChangesAsync();
        }

        public async Task DeleteStudentAsync(int id)
        {
            var student = await _DBcontext.Students.FindAsync(id);
            if (student != null)
            {
                _DBcontext.Students.Remove(student);
                await _DBcontext.SaveChangesAsync();
            }
        }


    }
}
