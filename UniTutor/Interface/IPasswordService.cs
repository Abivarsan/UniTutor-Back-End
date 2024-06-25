namespace UniTutor.Interface
{
    public interface IPasswordService
    {
        Task SendVerificationCodeAsync(string email);
        Task<bool> ResetPasswordAsync(string email, string verificationCode, string newPassword);
    }
}
