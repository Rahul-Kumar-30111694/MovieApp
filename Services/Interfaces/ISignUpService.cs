using MovieApp.Models;

namespace MovieApp.Services
{
    public interface ISignUpService
    {
        public bool SignUpMethod(SignUpModel request);
    }
}