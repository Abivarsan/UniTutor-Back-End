using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniTutor.Model
{
    public class Request
    {
        [Key]
        public int Id { get; set; }
        public string Location { get; set; }
        public string Subject { get; set; }
        public string Medium { get; set; }
        public string Availability { get; set; }
        public int status { get; set; } = 0;
        public int StudentId { get; set; }
        public int TutorId { get; set; }
        [NotMapped]
        public Tutor Tutor { get; set; }
        [NotMapped]
        public Student Student { get; set; }



    }
}
