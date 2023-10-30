using MainProject.Login.Model;
using MainProject.Login.Interface;
using MainProject.DataBase.Interface;
using MainProject.SignUp.Model;
using MongoDB.Driver;

namespace MainProject.Login.Service
{
    public class LoginServices : ILoginServices
    {
        private readonly IDatabaseCollections _databaseCollections;

        public LoginServices(IDatabaseCollections databaseCollections)
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