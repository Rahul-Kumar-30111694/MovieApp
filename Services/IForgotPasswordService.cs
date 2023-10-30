using MainProject.ForgotPassword.Model;

namespace MainProject.ForgotPassword.Interface
{
    public interface IForgotPasswordServices
    {
        public bool ForgotPasswordMethod(ForgotPasswordModel request);
    }
}