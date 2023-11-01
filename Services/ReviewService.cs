using MongoDB.Driver;
using MovieApp.Models;
using MovieApp.Database;
using Newtonsoft.Json;
using RestSharp;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace MovieApp.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IDatabaseCollections _databaseCollections;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ReviewService(IDatabaseCollections databaseCollections, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _databaseCollections = databaseCollections;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
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
        public bool StoreComment(string data, string IMDBID, int Stars)
        {
            Review review = new Review
            {
                Stars = Stars,
                Username = getusername(_httpContextAccessor?.HttpContext?.Request.Cookies["Token"]!),
                imdbID = IMDBID,
                Comments = data
            };
            _databaseCollections.AllComments().InsertOne(review);
            return true;
        }
        public string getusername(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWTTokentext:Token").Value!));
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                IssuerSigningKey = key
            };
            SecurityToken validatedToken;
            var principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
            string email = principal.FindFirst(ClaimTypes.Email)?.Value!;
            var user = _databaseCollections.UserDetails().Find(x => x.EmailAddress == email).SingleOrDefault();
            return user.Username!;
        }
        public bool DeleteCommentMethod(string ReviewID)
        {
            _databaseCollections.AllComments().DeleteOne(x => x.Id == ReviewID);
            return true;
        }
    }
}