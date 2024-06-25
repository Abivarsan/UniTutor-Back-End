using System.ComponentModel.DataAnnotations;

namespace UniTutor.Model
{
    public class TutorViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string password { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int Ocupation { get; set; }
        public string ModeOfTeaching { get; set; }
        public string Medium { get; set; }
        public string subject { get; set; }
        public string Qualification { get; set; }
        [Required]
        public int HomeTown { get; set; }

        public IFormFile CvFile { get; set; }

        public IFormFile UniIdFile { get; set; }
    }
}
