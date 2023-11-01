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
        public IActionResult HomePage(string request, string genre, string Year)
        {
            ViewBag.Role = rolereturn();
            if (!string.IsNullOrEmpty(Request.Cookies["Token"]))
            {
                if (string.IsNullOrEmpty(Year))
                {
                    if (string.IsNullOrEmpty(genre) && string.IsNullOrEmpty(request))
                    {
                        return View(_homePageService.GetByItem(request));
                    }
                    else if (string.IsNullOrEmpty(genre) && !string.IsNullOrEmpty(request))
                    {
                        return View(_homePageService.GetByItem(request));
                    }
                    else if (!string.IsNullOrEmpty(genre) && string.IsNullOrEmpty(request))
                    {
                        return View(_homePageService.GetByGenre(genre));
                    }
                    else
                    {
                        return View(_homePageService.GetByRequestandGenre(request, genre));
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(genre) && string.IsNullOrEmpty(request))
                    {
                        return View(_homePageService.GetByYear(Year));
                    }
                    else if (string.IsNullOrEmpty(genre) && !string.IsNullOrEmpty(request))
                    {
                        return View(_homePageService.GetByRequestandYear(request, Year));
                    }
                    else if (!string.IsNullOrEmpty(genre) && string.IsNullOrEmpty(request))
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