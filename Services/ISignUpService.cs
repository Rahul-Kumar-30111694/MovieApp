using MainProject.SignUp.Model;

namespace MainProject.SignUp.Interface
{
    public interface ISignUpServices
    {
        public bool SignUpMethod(SignUpModel request);
    }
}