using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace UniTutor.Model
{
    public class Student
    {

        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Grade { get; set; }
        public String Address { get; set; }
        public string School { get; set; }
        public int HomeTown { get; set; }
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

       [Required]
        public string password { get; set; }
        public String? VerificationCode { get; set; }

        public string? ProfileImageUrl { get; set; }
        

    }
}
