using MovieApp.Models;
using MongoDB.Driver;
using MovieApp.Database;
using MovieApp.Services;
using MovieApp.Methods;

namespace MainProject.Login.Service
{
    public class LoginService : ILoginService
    {
        private readonly IDatabaseCollections _databaseCollections;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IJWTMethod _jWTMethod;

        public LoginService(IDatabaseCollections databaseCollections, IHttpContextAccessor httpContextAccessor, IJWTMethod jWTMethod)
        {
            _databaseCollections = databaseCollections;
            _httpContextAccessor = httpContextAccessor;
            _jWTMethod = jWTMethod;
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
                if (request.Password == UserInDB!.Password)
                {
                    var result = _jWTMethod.CreateToken(UserInDB.EmailAddress!);
                    _httpContextAccessor?.HttpContext?.Response.Cookies.Append("Token", result);
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