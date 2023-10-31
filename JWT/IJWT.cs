using MovieApp.Models;

namespace MovieApp.Methods
{
    public interface IJWTMethod
    {
        public string CreateToken(string UserEmail);
        public string ValidateToken(string token);

    }
}