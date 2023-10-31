using Microsoft.AspNetCore.Mvc;
using MovieApp.Methods;
using MovieApp.Services;

namespace MovieApp.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;
        private readonly IJWTMethod _jWTMethod;
        public ReviewController(IReviewService reviewService, IJWTMethod jWTMethod)
        {
            _reviewService = reviewService;
            _jWTMethod = jWTMethod;
        }
        public IActionResult Review(string movieName)
        {
            var result = _reviewService.AboutMovie(movieName);
            ViewBag.ImdbRating = _reviewService.MovieimdbRating(movieName);
            ViewBag.Ratings = _reviewService.MovieRating(movieName);
            return View(result);
        }

        public IActionResult MyComment(string remarks, string IMDBID)
        {
            _reviewService.StoreComment(remarks, IMDBID);
            return Ok("Success");
        }
    }
}