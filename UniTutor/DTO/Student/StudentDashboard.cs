using System.ComponentModel.DataAnnotations;

namespace UniTutor.DTO.Student
{
    public class StudentDashboard
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Grade { get; set; }
        public String Address { get; set; }
        public int HomeTown { get; set; }
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
        public string? ProfileImage { get; set; }
    }
}
