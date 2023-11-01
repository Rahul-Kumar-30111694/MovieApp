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
            ViewBag.Role = _jWTMethod.ValidateToken(Request.Cookies["Token"]!);
            return View(result);
        }

        public IActionResult MyComment(string remarks, string IMDBID, int Stars)
        {
            if (_reviewService.StoreComment(remarks, IMDBID, Stars))
            {
                ViewBag.ErrorMessage = "Review Submitted";
                return RedirectToAction("HomePage", "HomePage");
            }
            else
            {
                ViewBag.ErrorMessage = "Something Went Wrong! Try again Later.";
                return RedirectToAction("HomePage", "HomePage");
            }
        }
        public IActionResult DeleteComment(string ReviewID)
        {
            if(_reviewService.DeleteCommentMethod(ReviewID))
            {
                return Content("Done");
            }
            else
            {
                return Content("False");
            }
        }
    }
}