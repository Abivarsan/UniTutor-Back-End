using System.ComponentModel.DataAnnotations;

namespace UniTutor.Model
{
    public class Tutor
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public string Ocupation { get; set; }
        public string ModeOfTeaching { get; set; }
        public string Medium { get; set;}
        public string subject { get; set; }
        public string Qualification { get; set; }

        [Required]
        public string Password { get; set; }
        [Required]
        public string HomeTown { get; set; }
        public string? Uni_ID { get; set; }
        public string[]? CV { get; set; }

      
    }
}
