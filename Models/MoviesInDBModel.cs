using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MovieApp.Models
{
    public class MoviesInDB
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string imdbID { get; set; }
        public string Title { get; set; }
        public string Poster { get; set; }
        public string Genre { get; set; }
        public int Year { get; set; }
        public string Director { get; set; }
        public string Actors { get; set; }
        public string Plot { get; set; }
    }
}