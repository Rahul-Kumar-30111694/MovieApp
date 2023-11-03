using MongoDB.Driver;
using MovieApp.Models;
using MovieApp.Database;
using Newtonsoft.Json;
using RestSharp;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text.RegularExpressions;
using MovieApp.Methods;

namespace MovieApp.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IDatabaseCollections _databaseCollections;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IJWTMethod _jWTMethod;
        public ReviewService(IDatabaseCollections databaseCollections, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IJWTMethod jWTMethod)
        {
            _databaseCollections = databaseCollections;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _jWTMethod = jWTMethod;
        }

        public ViewPage AboutMovie(string movieName)
        {
            var result = _databaseCollections.MovieDetails().Find(x => x.imdbID == movieName).SingleOrDefault();
            //var imdbid = result.imdbID;
            ViewPage viewPage = new ViewPage
            {
                Moviedata = result,
                MovieReviews = _databaseCollections.AllComments().Find(x => x.imdbID == movieName).ToList()
            };
            return viewPage;
        }
        public string MovieimdbRating(string movieName)
        {
            var client = new RestClient("http://www.omdbapi.com");
            var request = new RestRequest();
            request.AddParameter("apiKey", _configuration["apiKey"]);
            request.AddParameter("i", movieName);
            var response = client.Execute(request);
            MovieModel? MovieFromApi = JsonConvert.DeserializeObject<MovieModel>(response.Content);
            return MovieFromApi!.imdbRating;
        }
        public List<Rating> MovieRating(string movieName)
        {
            var client = new RestClient("http://www.omdbapi.com");
            var request = new RestRequest();
            request.AddParameter("apiKey", _configuration["apiKey"]);
            request.AddParameter("i", movieName);
            var response = client.Execute(request);
            MovieModel? MovieFromApi = JsonConvert.DeserializeObject<MovieModel>(response.Content);
            return MovieFromApi!.Ratings.ToList();
        }
        public bool StoreComment(string data, string IMDBID, int Stars)
        {
            Review review = new Review
            {
                Stars = Stars,
                Username = _jWTMethod.ValidateToken(_httpContextAccessor?.HttpContext?.Request.Cookies["Token"]!),
                imdbID = IMDBID,
                Comments = data
            };
            _databaseCollections.AllComments().InsertOne(review);
            return true;
        }
        public bool DeleteCommentMethod(string ReviewID)
        {
            _databaseCollections.AllComments().DeleteOne(x => x.Id == ReviewID);
            return true;
        }
        public bool IsIMDbId(string text)
        {
            string pattern = @"^tt\d{5}$";
            return Regex.IsMatch(text, pattern);
        }
    }
}