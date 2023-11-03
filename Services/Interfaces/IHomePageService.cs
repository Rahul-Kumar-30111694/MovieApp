using MongoDB.Driver;
using MovieApp.Models;

namespace MovieApp.Services
{
    public interface IHomePageService
    {
        // public List<MoviesInDB> GenreFilter(string filterdata);
        public List<MoviesInDB> GetByItem(string data);
        public List<MoviesInDB> GetByYear(string Year);
        public List<MoviesInDB> GetByRequestandGenre(string request, List<string> genre);
        public List<MoviesInDB> GetByRequestandYear(string request, string Year);
        public List<MoviesInDB> GetByGenreandYear(List<string> genre, string Year);
        public List<MoviesInDB> GetAll(string request, List<string> genre, string Year);
        public List<MoviesInDB> Search(string request);
        public List<MoviesInDB> GetMoviesByGenres(List<string> genres);
        public FilterDefinition<MoviesInDB> ReturnGenres(List<string> genres);
    }
}