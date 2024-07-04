using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace UniTutor.Model
{
    public class Student
    {

        [Key]
        public int Id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string grade { get; set; }
        public string schoolName { get; set; }
        public string address { get; set; }
       
        public string district { get; set; }
        public string phoneNumber { get; set; }

        [Required]
        [MaxLength(100)]
        public string email { get; set; }

       [Required]
        public string password { get; set; }
        public string? VerificationCode { get; set; }

        public string? ProfileImageUrl { get; set; }
        public DateTime? CreationDate { get; set;}
        public int?  numberofcomplain {  get; set; }

        //navigation Property
        //public ICollection<Comment> Comments { get; set; }
    }
}
