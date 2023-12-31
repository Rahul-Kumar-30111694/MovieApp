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
            if (data == null)
            {
                return _databaseCollections.MovieDetails().Find(_ => true).ToList();
            }
            else
            {

                return Search(data);
            }
        }
        // public List<MoviesInDB> GetByGenre(string genre)
        // {
        //     return _databaseCollections.MovieDetails().Find(Builders<MoviesInDB>.Filter.Regex("Genre", new BsonRegularExpression(genre, "i"))).ToList();
        // }
        public List<MoviesInDB> GetMoviesByGenres(List<string> genres)
        {
            FilterDefinition<MoviesInDB> MovieFilter = Builders<MoviesInDB>.Filter.Empty;;
            //var result = genres.ToString();
            foreach(var item in genres)
            {
                var GenreFilter = Builders<MoviesInDB>.Filter.Regex("Genre", new BsonRegularExpression(item, "i"));
                MovieFilter = MovieFilter & GenreFilter;
            }
            if(MovieFilter == Builders<MoviesInDB>.Filter.Empty)
            {
                return new List<MoviesInDB>();
            }
            else
            {
                return _databaseCollections.MovieDetails().Find(MovieFilter).ToList();
            }
        }
        public FilterDefinition<MoviesInDB> ReturnGenres(List<string> genres)
        {
            FilterDefinition<MoviesInDB> MovieFilter = Builders<MoviesInDB>.Filter.Empty;;
            foreach(var item in genres)
            {
                var GenreFilter = Builders<MoviesInDB>.Filter.Regex("Genre", new BsonRegularExpression(item, "i"));
                MovieFilter = MovieFilter & GenreFilter;
            }
            return MovieFilter;
        }
        public List<MoviesInDB> GetByYear(string Year)
        {
            return _databaseCollections.MovieDetails().Find(Builders<MoviesInDB>.Filter.Regex("Year", new BsonRegularExpression(Year, "i"))).ToList();
        }
        public List<MoviesInDB> GetByRequestandGenre(string request, List<string> genres)
        {
            var SearchFilter = Builders<MoviesInDB>.Filter.Regex("Title", new BsonRegularExpression(request, "i"));
            var GenreFilter = ReturnGenres(genres);
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
        public List<MoviesInDB> GetByGenreandYear(List<string> genres, string Year)
        {
            var GenreFilter = ReturnGenres(genres);
            var YearFilter = Builders<MoviesInDB>.Filter.Regex("Year", new BsonRegularExpression(Year, "i"));
            var MovieFilter = GenreFilter & YearFilter;
            var result = _databaseCollections.MovieDetails().Find(MovieFilter).ToList();
            return result;
        }
        public List<MoviesInDB> GetAll(string request, List<string> genres, string Year)
        {
            var SearchFilter = Builders<MoviesInDB>.Filter.Regex("Title", new BsonRegularExpression(request, "i"));
            var GenreFilter = ReturnGenres(genres);
            var YearFilter = Builders<MoviesInDB>.Filter.Regex("Year", new BsonRegularExpression(Year, "i"));
            var MovieFilter = SearchFilter & GenreFilter & YearFilter;
            var result = _databaseCollections.MovieDetails().Find(MovieFilter).ToList();
            return result;
        }
        public List<MoviesInDB> Search(string request)
        {
            List<MoviesInDB> result = new();
            var filters = new List<FilterDefinition<MoviesInDB>>();
            filters.Add(Builders<MoviesInDB>.Filter.Regex("Title", new BsonRegularExpression(request, "i")));
            filters.Add(Builders<MoviesInDB>.Filter.Regex("Actor", new BsonRegularExpression(request, "i")));
            filters.Add(Builders<MoviesInDB>.Filter.Regex("Director", new BsonRegularExpression(request, "i")));
            if (filters.Count > 0)
            {
                var combinedFilter = Builders<MoviesInDB>.Filter.Or(filters);
                result = _databaseCollections.MovieDetails().Find(combinedFilter).ToList();
            }
            return result;
        }
    }
}