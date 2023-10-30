using MongoDB.Driver;
using MovieApp.Models;

namespace MovieApp.Database
{
    public class DatabaseCollections : IDatabaseCollections
    {
        private readonly IMongoCollection<MoviesInDB> _MovieData;
        private readonly IMongoCollection<Review> _Reviews;
        public DatabaseCollections(IConfiguration configuration)
        {
            var _mongoClient = new MongoClient(configuration["Database:ConnectionString"]);
            var _mongoDatabase = _mongoClient.GetDatabase(configuration["Database:DatabaseName"]);
            _MovieData = _mongoDatabase.GetCollection<MoviesInDB>(configuration["Database:MovieDataCollectionName"]);
            _Reviews = _mongoDatabase.GetCollection<Review>(configuration["Database:ReviewDataCollectionName"]);
        }

        public IMongoCollection<MoviesInDB> MovieDetails()
        {
            return _MovieData;
        }
        public IMongoCollection<Review> AllComments()
        {
            return _Reviews;
        }
    }
}