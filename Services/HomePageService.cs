using MongoDB.Bson;
using MongoDB.Driver;
using MovieApp.Models;
using MovieApp.Database;

namespace MovieApp.Services
{
    public class HomePageService : IHomePageService
    {
        private readonly IDatabaseCollections _databaseCollections;
        public HomePageService(IDatabaseCollections databaseCollections)
        {
            _databaseCollections = databaseCollections;
        }
        public List<MoviesInDB> GetByItem(string data)
        {
            if(data == null)
            {
                return _databaseCollections.MovieDetails().Find(_ => true).ToList();
            }
            else
            {
                return _databaseCollections.MovieDetails().Find(x => x.Title == data).ToList();
            }
        }
        public List<MoviesInDB> GetByGenre(string genre)
        {
            return _databaseCollections.MovieDetails().Find(Builders<MoviesInDB>.Filter.Regex("Genre", new BsonRegularExpression(genre, "i"))).ToList();
        }
        public List<MoviesInDB> GetByRequestandGenre(string request, string genre)
        {
            var SearchFilter = Builders<MoviesInDB>.Filter.Regex("Title", new BsonRegularExpression(request, "i"));
            var GenreFilter = Builders<MoviesInDB>.Filter.Regex("Genre", new BsonRegularExpression(genre, "i"));
            var MovieFilter = SearchFilter & GenreFilter;
            var result = _databaseCollections.MovieDetails().Find(MovieFilter).ToList();
            return result;
        }
        public List<MoviesInDB> GetByRequestandYear(string request, string Year)
        {
            var SearchFilter = Builders<MoviesInDB>.Filter.Regex("Title", new BsonRegularExpression(request, "i"));
            var YearFilter = Builders<MoviesInDB>.Filter.Regex("Year", new BsonRegularExpression(Year, "i"));
            var MovieFilter = SearchFilter & YearFilter;
            var result = _databaseCollections.MovieDetails().Find(MovieFilter).ToList();
            return result;
        }
        public List<MoviesInDB> GetByGenreandYear(string genre, string Year)
        {
            var GenreFilter = Builders<MoviesInDB>.Filter.Regex("Genre", new BsonRegularExpression(genre, "i"));
            var YearFilter = Builders<MoviesInDB>.Filter.Regex("Year", new BsonRegularExpression(Year, "i"));
            var MovieFilter = GenreFilter & YearFilter;
            var result = _databaseCollections.MovieDetails().Find(MovieFilter).ToList();
            return result;
        }
        public List<MoviesInDB> GetAll(string request, string genre, string Year)
        {
            var SearchFilter = Builders<MoviesInDB>.Filter.Regex("Title", new BsonRegularExpression(request, "i"));
            var GenreFilter = Builders<MoviesInDB>.Filter.Regex("Genre", new BsonRegularExpression(genre, "i"));
            var YearFilter = Builders<MoviesInDB>.Filter.Regex("Year", new BsonRegularExpression(Year, "i"));
            var MovieFilter = SearchFilter & GenreFilter & YearFilter;
            var result = _databaseCollections.MovieDetails().Find(MovieFilter).ToList();
            return result;
        }
    }
}