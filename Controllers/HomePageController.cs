using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MovieApp.Models;
using MovieApp.Services;
using MovieApp.Database;

namespace MovieApp.Controllers
{
    public class HomePageController : Controller
    {
        private readonly IHomePageService _homePageService;
        public HomePageController(IHomePageService homePageService)
        {
            _homePageService = homePageService;
        }

        // public ActionResult HomePage(string request, int page = 1, int pageSize = 10)
        // {
        //     List<MoviesInDB> movies;

        //     if (!string.IsNullOrEmpty(request))
        //     {
        //         ViewBag.SearchTerm = request;
        //         movies = GetMoviesBySearchTerm(request);
        //     }
        //     else
        //     {
        //         ViewBag.SearchTerm = "";
        //         movies = _databaseCollections.MovieDetails().Find(_ => true).ToList(); 
        //     }

        //     var pagedMovies = Paginate(movies, page, pageSize);

        //     return View(new PagedMoviesViewModel
        //     {
        //         Movies = pagedMovies,
        //         CurrentPage = page,
        //         TotalPages = (int)Math.Ceiling((double)movies.Count / pageSize)
        //     });
        // }
        // private List<MoviesInDB> Paginate(List<MoviesInDB> movies, int page, int pageSize)
        // {
        //     int skip = (page - 1) * pageSize;
        //     return movies.Skip(skip).Take(pageSize).ToList();
        // }
        // private List<MoviesInDB> GetMoviesBySearchTerm(string searchTerm)
        // {
        //     return _databaseCollections.MovieDetails().Find(x => x.Title == searchTerm).ToList();
        // }
        // // public IActionResult GFilter(string genre)
        // // {
        // //     _homePageService.GenreFilter(genre);
        // //     return Content(genre);
        // // }
        // public ActionResult GFilter(string genre, int page = 1, int pageSize = 10)
        // {
        //     List<MoviesInDB> movies;

        //     if (!string.IsNullOrEmpty(genre))
        //     {
        //         ViewBag.SearchTerm = genre;
        //         movies = GetMoviesBySearchTerm(genre);
        //     }
        //     else
        //     {
        //         ViewBag.SearchTerm = "";
        //         movies = _databaseCollections.MovieDetails().Find(_ => true).ToList(); 
        //     }

        //     var pagedMovies = Paginate(movies, page, pageSize);

        //     return View(new PagedMoviesViewModel
        //     {
        //         Movies = pagedMovies,
        //         CurrentPage = page,
        //         TotalPages = (int)Math.Ceiling((double)movies.Count / pageSize)
        //     });
        // }
    
        public IActionResult HomePage(string request,string genre, string Year)
        {
            //ViewBag.Role = TempData["Message"] as string;
            // if(Year == null)
            // {
            //     if(genre == null)
            //     {

            //     return View(_homePageService.GetByItem(request));
            //     }
            //     else
            //     {
            //         return View(_homePageService.GetByGenre(genre));
            //     }
            // }
            // else if(request == null && Year == null)
            // {
            //     return View(_homePageService.GetByGenre(genre));
            // }
            // else if(Year == null)
            // {
            //     return View(_homePageService.GetByRequestandGenre(request, genre));
            // } 
            if(string.IsNullOrEmpty(Year))
            {
                if(string.IsNullOrEmpty(genre) && string.IsNullOrEmpty(request))
                {
                    return View(_homePageService.GetByItem(request));
                }
                else if(string.IsNullOrEmpty(genre) && !string.IsNullOrEmpty(request))
                {
                    return View(_homePageService.GetByItem(request));
                }
                else if(!string.IsNullOrEmpty(genre) && string.IsNullOrEmpty(request))
                {
                    return View(_homePageService.GetByGenre(genre));
                }
                else
                {
                    return View(_homePageService.GetByRequestandGenre(request, genre));
                }
            }
            else if(!string.IsNullOrEmpty(Year))
            {
                if(string.IsNullOrEmpty(genre) && string.IsNullOrEmpty(request))
                {

                }
                else if(string.IsNullOrEmpty(genre) && !string.IsNullOrEmpty(request))
                {

                }
                else if(!string.IsNullOrEmpty(genre) && string.IsNullOrEmpty(request))
                {

                }
                else
                {

                }
            }
            return Ok();
        }
        public IActionResult Logout()
        {
            Response.Cookies.Delete("Token");
            return RedirectToAction("Login", "Login");
        }
        // public IActionResult GFilter(string genre)
        // {
        //     return RedirectToAction("HomePage", "HomePage", _homePageService.GetByGenre(genre));
        // }
    }
}