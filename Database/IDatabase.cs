using MongoDB.Driver;
using MovieApp.Models;

namespace MovieApp.Database
{
    public interface IDatabaseCollections
    {
        public IMongoCollection<MoviesInDB> MovieDetails();
        public IMongoCollection<Review> AllComments();
        public IMongoCollection<SignUpModel> UserDetails();
    }
}