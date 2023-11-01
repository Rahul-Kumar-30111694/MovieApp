using MongoDB.Driver;
using MovieApp.Models;
using MovieApp.Database;
using Newtonsoft.Json;
using RestSharp;

namespace MovieApp.Services
{
    public class AdminService : IAdminService
    {
        private readonly IDatabaseCollections _databaseCollections;
        private readonly IConfiguration _configuration;
        public AdminService(IDatabaseCollections databaseCollections, IConfiguration configuration)
        {
            _databaseCollections = databaseCollections;
            _configuration = configuration;
        }

        public bool AdminMethods(MoviesInDB movierequest)
        {
            var movie = _databaseCollections.MovieDetails().Find(x => x.imdbID == movierequest.imdbID).SingleOrDefault();
            if (movie != null)
            {
                return false;
            }
            else
            {
                var client = new RestClient("http://www.omdbapi.com");
                var request = new RestRequest();
                request.AddParameter("apiKey", _configuration["apiKey"]);
                request.AddParameter("i", movierequest.imdbID);
                var response = client.Execute(request);
                MovieModel? MovieFromApi = JsonConvert.DeserializeObject<MovieModel>(response.Content);
                movierequest.Poster = MovieFromApi!.Poster;
                movierequest.imdbID = MovieFromApi.imdbID;
                _databaseCollections.MovieDetails().InsertOne(movierequest);
                return true;
            }
        }
    }
}