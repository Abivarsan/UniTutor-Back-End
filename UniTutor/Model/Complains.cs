using System.ComponentModel.DataAnnotations;

namespace UniTutor.Model
{
    
        public class Comment
        {
            public int Id { get; set; }
            public int StudentId { get; set; }
            public string Content { get; set; }
            public DateTime CreatedAt { get; set; }

            // Navigation property
            public Student Student { get; set; }
        }

    
}
