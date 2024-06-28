using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime;

namespace UniTutor.Model
{
    public class Tutor
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }
        public string password { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int Ocupation { get; set; }
        public string ModeOfTeaching { get; set; }
        public string Medium { get; set;}
        public string subject { get; set; }
        public string Qualification { get; set; }
        public int HomeTown { get; set; }
        public string UniIdUrl { get; set; }   
        public string CVUrl { get; set; }
        
        public int accept { get; set; }=0;
        public string? VerificationCode { get; set; }


    }
}
