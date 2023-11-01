using MovieApp.Models;

namespace MovieApp.Services
{
    public interface ILoginService
    {
       public bool LoginMethod(LoginModel request);
    }
}