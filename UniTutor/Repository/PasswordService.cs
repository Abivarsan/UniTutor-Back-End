//using UniTutor.Interface;
//using UniTutor.Model;

//namespace UniTutor.Repository
//{
//    public class PasswordService : IPasswordService
//    {
//        private readonly IStudent _student;
//        private readonly ITutor _tutor;
//        private readonly IEmailService _emailService;

//        public PasswordService(IStudent student, ITutor tutor, IEmailService emailService)
//        {
//            _student = student;
//            _tutor = tutor;
//            _emailService = emailService;
//        }

//        public async Task SendVerificationCodeAsync(string email)
//        {
//            var user = _student.GetByMail(email) ?? _tutor.GetTutorByEmail(email);
//            if (user == null)
//            {
//                throw new Exception("Email not found");
//            }

//            // Generate verification code
//            var verificationCode = Guid.NewGuid().ToString("N").Substring(0, 6);

//            // Save verification code to the database
//            user.VerificationCode = verificationCode;
//            await (user is Student ? _student.Update((Student)user) : _tutor.Update((Tutor)user));

//            // Send verification code via email
//            await _emailService.SendVerificationCodeAsync(user.Email, verificationCode);
//        }

//        public async Task<bool> ResetPasswordAsync(string email, string verificationCode, string newPassword)
//        {
//            var user = _student.GetByMail(email) ?? _tutor.GetTutorByEmail(email);
//            if (user == null || user.VerificationCode != verificationCode)
//            {
//                return false;
//            }

//            // Update the password
//            PasswordHash ph = new PasswordHash();
//            user.Password = ph.HashPassword(newPassword);
//            user.VerificationCode = null; // Clear the verification code after successful reset

//            await (user is Student ? _student.Update((Student)user) : _tutor.Update((Tutor)user));
//            return true;
//        }
//    }
//}
