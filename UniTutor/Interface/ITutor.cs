using UniTutor.DTO;
using UniTutor.Model;

namespace UniTutor.Interface
{
    public interface ITutor
    {
        public bool Delete(int id);
       public Tutor GetById(int id);
        public IEnumerable<Tutor> GetAll();
        public bool login(string email, string password);
        public bool SignUp(Tutor tutor);

        public bool logout();
        public bool acceptRequest(Request request);

       public bool rejectRequest(Request request);
        public ICollection<Request> GetAllRequest(int id);
        public ICollection<Request> GetAcceptedRequest(int id);

       public Tutor GetTutorByEmail(string email);

        public bool isUser(string email);
        Task<Tutor> GetTutorAsync(int id);
        Task UpdateTutorAsync(Tutor tutor);
        public Task<Tutor> UpdateTutorProfile(int id, UpdateTutor updatedtutor);





        // public bool createComplaint(Complaint complaint);


    }
}
