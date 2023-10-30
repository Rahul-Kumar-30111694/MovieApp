using Microsoft.AspNetCore.Mvc;
using MovieApp.Services;

namespace MovieApp.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
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