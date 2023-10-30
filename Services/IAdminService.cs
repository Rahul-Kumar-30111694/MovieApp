using MovieApp.Models;

namespace MovieApp.Services
{
    public interface IAdminService
    {
        public bool AdminMethods(MoviesInDB request);
    }
}