using MovieApp.Models;

namespace MovieApp.Services
{
    public interface IHomePageService
    {
        // public List<MoviesInDB> GenreFilter(string filterdata);
        public List<MoviesInDB> GetByItem(string data);
        public List<MoviesInDB> GetByGenre(string genre);
        public List<MoviesInDB> GetByYear(string Year);
        public List<MoviesInDB> GetByRequestandGenre(string request, string genre);
        public List<MoviesInDB> GetByRequestandYear(string request, string Year);
        public List<MoviesInDB> GetByGenreandYear(string genre, string Year);
        public List<MoviesInDB> GetAll(string request, string genre, string Year);
        public List<MoviesInDB> Search(string request);
    }
}