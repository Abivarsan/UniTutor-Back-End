namespace UniTutor.Model
{
    public class StudentViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Grade { get; set; }
        public String Address { get; set; }
        public int HomeTown { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string password { get; set; }
        public String? VerificationCode { get; set; }
        public IFormFile Image { get; set; }
    }
}
