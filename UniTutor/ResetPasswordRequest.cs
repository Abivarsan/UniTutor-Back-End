namespace UniTutor
{
    public class ResetPasswordRequest
    {
        public string Email { get; set; }
        public string VerificationCode { get; set; } // Make sure this property is defined
        public string NewPassword { get; set; }
    }
}
