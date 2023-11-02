using Microsoft.AspNetCore.Mvc;
using MovieApp.Methods;
using MovieApp.Services;

namespace MovieApp.Controllers
{
    public class HomePageController : Controller
    {
        private readonly IHomePageService _homePageService;
        private readonly IJWTMethod _jWTMethod;
        public HomePageController(IHomePageService homePageService, IJWTMethod jWTMethod)
        {
            _homePageService = homePageService;
            _jWTMethod = jWTMethod;
        }
        public IActionResult HomePage(string request, List<string> genre, string Year)
        {
            ViewBag.Message = TempData["Message"]?.ToString();
            ViewBag.Role = rolereturn();
            if (!string.IsNullOrEmpty(Request.Cookies["Token"]))
            {
                if (string.IsNullOrEmpty(Year))
                {
                    if ((genre == null || genre.Count == 0) && string.IsNullOrEmpty(request))
                    {
                        return View(_homePageService.GetByItem(request));
                    }
                    else if ((genre == null || genre.Count == 0) && !string.IsNullOrEmpty(request))
                    {
                        return View(_homePageService.GetByItem(request));
                    }
                    else if ((genre != null || genre.Count != 0) && string.IsNullOrEmpty(request))
                    {
                        return View(_homePageService.GetMoviesByGenres(genre));
                    }
                    else
                    {
                        return View(_homePageService.GetByRequestandGenre(request, genre));
                    }
                }
                else
                {
                    if ((genre == null || genre.Count == 0) && string.IsNullOrEmpty(request))
                    {
                        return View(_homePageService.GetByYear(Year));
                    }
                    else if ((genre == null || genre.Count == 0) && !string.IsNullOrEmpty(request))
                    {
                        return View(_homePageService.GetByRequestandYear(request, Year));
                    }
                    else if ((genre != null || genre.Count != 0) && string.IsNullOrEmpty(request))
                    {
                        return View(_homePageService.GetByGenreandYear(genre, Year));
                    }
                    else
                    {
                        return View(_homePageService.GetAll(request, genre, Year));
                    }
                }
            }
            else
            {
                return Content("UNAUTHORIZED");
            }
        }
        public string rolereturn()
        {
            return _jWTMethod.ValidateToken(Request.Cookies["Token"]!);
        }

    }
}