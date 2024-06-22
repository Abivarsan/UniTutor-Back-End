using UniTutor.Model;

namespace UniTutor.Interface
{
    public interface ITutor
    {
        public bool Delete(int id);
       public Tutor GetById(int id);
        public IEnumerable<Tutor> GetAll();
        public bool login(string email, string password);
        public bool signUp(Tutor tutor);
        public bool logout();
       // public bool acceptProject(Project project);
       // public bool rejectProject(Project project);

       public Tutor GetTutorByEmail(string email);

        public bool isUser(string email);

        //public ICollection<Project> GetProjects(int id);

       // public bool createComplaint(Complaint complaint);

       //public string UploadFile(IFormFile file);

       // public string[] UploadFiles(IFormFile[] files);
    }
}
