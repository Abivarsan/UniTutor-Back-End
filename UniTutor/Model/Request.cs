using System.ComponentModel.DataAnnotations;

namespace UniTutor.Model
{
    public class Request
    {
        [Key]
        public int RequestId { get; set; }
        
        public string Location { get; set; }
        public string Subject { get; set; }
        public string Medium { get; set; }
        public string Availability { get; set; }
        public bool status { get; set; }
        public int Student_Id { get; set; }
        public Student Student { get; set; }
        public int Tutor_id { get; set; }
        public Tutor Tutor { get; set; }


    }
}
