using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniTutor.Model
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }
        public string  title { get; set; }
        public string description { get; set; }
        public string coverImage { get; set; }
        public string[] medium {  get; set; }
        public string mode { get; set; }
        public string[] availability {  get; set; }
        public int tutorId { get; set; }
       
        public Tutor Tutor { get; set; }

    }
}
