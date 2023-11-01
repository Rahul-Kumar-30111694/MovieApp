using System.ComponentModel;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MovieApp.Models
{
    public class Review
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Username { get; set; }  
        public int Stars { get; set; }
        public DateTime DatePosted { get; set; }
        public string imdbID { get; set; }
        public string Comments { get; set; }
    }

    public class ViewPage
    {
        public MoviesInDB Moviedata { get; set; }
        public List<Review> MovieReviews { get; set; }
    }
}