//using System.ComponentModel.DataAnnotations.Schema;
//using System.ComponentModel.DataAnnotations;

//namespace UniTutor.Model
//{
//    public class Review
//    {
//        [Key]
//        public int Id { get; set; }

//        // Foreign key for Tutor
//        public int TutorId { get; set; }
//        [ForeignKey("TutorId")]
//        public Tutor Tutor { get; set; }

//        // Foreign key for Student
//        public int StudentId { get; set; }
//        [ForeignKey("StudentId")]
//        public Student Student { get; set; }

//        // Foreign key for Subject
//        public int SubjectId { get; set; }
//        [ForeignKey("SubjectId")]
//        public Subject Subject { get; set; }

//        public int Rating { get; set; }

//        [MaxLength(1000)]
//        public string Feedback { get; set; }

//        public DateTime TimeStamp { get; set; }
//    }
//}
