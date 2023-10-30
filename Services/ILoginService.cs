using MainProject.Login.Model;

namespace MainProject.Login.Interface
{
    public interface ILoginService
    {
       public bool LoginMethod(LoginModel request);
    }
}