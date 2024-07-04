using Azure.Core;
using UniTutor.DTO;
using UniTutor.Model;

namespace UniTutor.Interface
{
    public interface IStudent
    {
        public bool SignUp(Student student);
        public bool Login(string email, string password);
        public Student GetByMail(string Email);
        // for delete 
        bool Delete(int id);
        public Student GetById(int id);
        public bool SignOut();
        public bool CreateRequest(Model.Request request);
        public bool DeleteRequest(Model.Request request);
        public Task<bool> Update(Student student);
        Task<Student> GetStudentAsync(int id);
        Task AddStudentAsync(Student student);
        
        Task DeleteStudentAsync(int id);
        public Task<Student> UpdateStudentProfile(int id, UpdateStudent updatedStudent);





    }
}
