using MovieApp.Models;

namespace MovieApp.Methods
{
    public interface IJWTMethod
    {
        public string CreateToken(string UserEmail);
        public SignUpModel ValidateToken(string token);
        //public string getEmail(string token);
    }
}