using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniTutor.DataBase;
using UniTutor.DTO;
using UniTutor.Interface;
using UniTutor.Model;

namespace UniTutor.Repository
{
    public class StudentRepository : IStudent
    {
        private ApplicationDBContext _DBcontext;
        private readonly IConfiguration _config;
       
        public StudentRepository(ApplicationDBContext DBcontext , IConfiguration config)
        {
            _DBcontext = DBcontext;
            
        }

        public bool SignUp(Student student)
        {
            try
            {
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
                var student = _DBcontext.Students.FirstOrDefault(a => a.email == email);

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
            return _DBcontext.Students.FirstOrDefault(s => s.email == Email);
        }
        public Student GetById(int id)
        {
            return _DBcontext.Students.Find(id);
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
                _DBcontext.Requests.Add(request);
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
                _DBcontext.Requests.Remove(request);
                _DBcontext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public ICollection<Tutor> GetAllTutor()
        {
            var tutors = _DBcontext.Tutors.ToList();
            return tutors;
        }
        //public ICollection<Tutor> GetTutorByLocation(int location)
        //{
        //    var tutors = _DBcontext.Tutors.Where(r => r.HomeTown == location).ToList();
        //    return tutors;
        //}


        public async Task<bool> Update(Student student)
        {
            _DBcontext.Students.Update(student);
            return await _DBcontext.SaveChangesAsync() > 0;
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
        public async Task<Student> UpdateStudentProfile(int id, UpdateStudent updatedStudent)
        {
            var student = await _DBcontext.Students.FindAsync(id);
            if (student == null)
            {
                return null;
            }

            if (updatedStudent.firstName != null)
            {
                student.firstName = updatedStudent.firstName;
            }
            if (updatedStudent.lastName != null)
            {
                student.lastName = updatedStudent.lastName;
            }
            if (updatedStudent.grade != null)
            {
                student.grade = updatedStudent.grade;
            }


            if (updatedStudent.address != null)
            {
                student.address = updatedStudent.address;
            }
            
            _DBcontext.Students.Update(student);
            await _DBcontext.SaveChangesAsync();

            return student;
        }

    }
}
