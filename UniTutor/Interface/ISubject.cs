using UniTutor.DTO;
using UniTutor.Model;

namespace UniTutor.Interface
{
    public interface ISubject
    {
        
        Task<bool> CreateSubject(int tutorId, SubjectRequest request);
       
        public  Task<Subject> GetSubjectById(int id);
        public Task<Subject> GetSubject(int tutorId, int id);
        
        public  Task<List<Subject>> GetSubjectsByTutorId(int tutorId);


    }
}
