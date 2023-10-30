using MovieApp.Models;

namespace MovieApp.Services
{
    public interface IForgotPasswordService
    {
        public bool ForgotPasswordMethod(ForgotPasswordModel request);
    }
}