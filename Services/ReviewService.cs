using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;
using MongoDB.Driver;
using MovieApp.Models;
using MovieApp.Database;
using Newtonsoft.Json;
using RestSharp;

namespace MovieApp.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IDatabaseCollections _databaseCollections;
        private readonly IConfiguration _configuration;
        public ReviewService(IDatabaseCollections databaseCollections,IConfiguration configuration)
        {
            _databaseCollections = databaseCollections;
            _configuration = configuration;
        }

        public ViewPage AboutMovie(string movieName)
        {
            var result = _databaseCollections.MovieDetails().Find(x => x.Title == movieName).SingleOrDefault();
            var imdbid = result.imdbID;
            ViewPage viewPage = new ViewPage
            {
                Moviedata = result,
                MovieReviews = _databaseCollections.AllComments().Find(x => x.imdbID == imdbid).ToList()
            };
            return viewPage;
        }
        public string MovieimdbRating(string movieName)
        {
            var client = new RestClient("http://www.omdbapi.com");
            var request = new RestRequest();
            request.AddParameter("apiKey", _configuration["apiKey"]);
            request.AddParameter("t", movieName);
            var response = client.Execute(request);
            MovieModel? MovieFromApi = JsonConvert.DeserializeObject<MovieModel>(response.Content);
            return MovieFromApi!.imdbRating;
        }
        public List<Rating> MovieRating(string movieName)
        {
            var client = new RestClient("http://www.omdbapi.com");
            var request = new RestRequest();
            request.AddParameter("apiKey", _configuration["apiKey"]);
            request.AddParameter("t", movieName);
            var response = client.Execute(request);
            MovieModel? MovieFromApi = JsonConvert.DeserializeObject<MovieModel>(response.Content);
            return MovieFromApi!.Ratings.ToList();
        }
        public bool StoreComment(string data, string IMDBID)
        {
            Review review = new Review{
                Username = null,
                imdbID = IMDBID,
                Comments = data
            };
            _databaseCollections.AllComments().InsertOne(review);
            return true;
        }
    }
}