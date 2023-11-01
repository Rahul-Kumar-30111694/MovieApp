using Microsoft.AspNetCore.Mvc;
using MovieApp.Methods;
using MovieApp.Models;
using MovieApp.Services;

namespace MovieApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IJWTMethod _jWTMethod;
        public AdminController(IAdminService adminService, IJWTMethod jWTMethod)
        {
            _adminService = adminService;
            _jWTMethod = jWTMethod;
        }
        public IActionResult Admin()
        {
            if (_jWTMethod.ValidateToken(Request.Cookies["Token"]) == "Admin")
            {
                return View();
            }
            else
            {
                return Content("UNAUTHORIZED");
            }
        }

        public IActionResult AdminControl(MoviesInDB request)
        {
            if(_adminService.AdminMethods(request))
            {   
                ViewBag.Message = "Successfull";
                return RedirectToAction("Admin");
            }
            else
            {
                ViewBag.Message = "Something went Wrong.";
                return RedirectToAction("Admin");
            }
        }
    }
}