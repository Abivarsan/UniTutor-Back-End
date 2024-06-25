namespace UniTutor.Interface
{
    public interface IEmailService
    {  
        Task SendVerificationCodeAsync(string email, string verificationCode);
  
    }
}
