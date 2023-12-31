using MovieApp.Models;

namespace MovieApp.Services
{
    public interface IReviewService
    {
        public ViewPage AboutMovie(string movieName);
        public string MovieimdbRating(string movieName);
        public List<Rating> MovieRating(string movieName);
        public bool StoreComment(string Comment, string imdbID, int Stars);
        public bool DeleteCommentMethod(string ReviewID);
        public bool IsIMDbId(string text);
    }
}