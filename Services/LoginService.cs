using MovieApp.Models;
using MongoDB.Driver;
using MovieApp.Database;
using MovieApp.Services;

namespace MainProject.Login.Service
{
    public class LoginService : ILoginService
    {
        private readonly IDatabaseCollections _databaseCollections;

        public LoginService(IDatabaseCollections databaseCollections)
        {
            _databaseCollections = databaseCollections;
        }
        public bool LoginMethod(LoginModel request)
        {
            SignUpModel UserInDB = _databaseCollections.UserDetails().Find(x => x.EmailAddress == request.EmailAddress).SingleOrDefault();
            if (UserInDB == null)
            {
                return false;
            }
            else
            {
                if(request.Password == UserInDB!.Password)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            } 
        }
    }
}