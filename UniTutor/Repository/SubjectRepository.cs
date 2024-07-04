using Microsoft.EntityFrameworkCore;
using UniTutor.DataBase;
using UniTutor.DTO;
using UniTutor.Interface;
using UniTutor.Model;

namespace UniTutor.Repository
{
    public class SubjectRepository: ISubject
    {
        private ApplicationDBContext _DBcontext;
        private readonly IConfiguration _config;

        public SubjectRepository(ApplicationDBContext DBcontext, IConfiguration config)
        {
            _DBcontext = DBcontext;
            _config = config;

        }
        
        public async Task<bool> CreateSubject(int tutorId, SubjectRequest request)
        {
            var tutor = await _DBcontext.Tutors.FindAsync(tutorId);
            if (tutor == null)
            {
                return false; // Tutor not found
            }

            var subject = new Subject
            {
                title = request.title,
                description = request.description,
                coverImage = request.coverImage,
                medium = request.medium,
                mode = request.mode,
                availability = request.availability,
                tutorId = tutorId,

             };

            _DBcontext.Subjects.Add(subject);
            await _DBcontext.SaveChangesAsync();
            return true;
        }
        public async Task<Subject> GetSubjectById(int id)
        {
            return await _DBcontext.Subjects.FindAsync(id);
        }
        public async Task<Subject> GetSubject(int tutorId, int id)
        {
            var subject = await GetSubjectById(id);
            if (subject == null || subject.tutorId != tutorId)
            {
                return null; // Subject not found or doesn't belong to the tutor
            }
            return subject;
        }

        public async Task<List<Subject>> GetSubjectsByTutorId(int tutorId)
        {
            return await _DBcontext.Subjects.Where(s => s.tutorId == tutorId).ToListAsync();
        }


    }
}
