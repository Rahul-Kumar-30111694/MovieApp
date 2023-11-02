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
                ViewBag.Message = TempData["Message"]?.ToString();
                return View();
            }
            else
            {
                TempData["Message"] = "Unauthorized Access Denied.";
                return RedirectToAction("Index", "Login");
            }
        }

        public IActionResult AdminControl(MoviesInDB request)
        {
            if(_adminService.AdminMethods(request))
            {   
                TempData["Message"] = "Successfull.";
                return RedirectToAction("Admin");
            }
            else
            {
                TempData["Message"] = "Failed.";
                return RedirectToAction("Admin");
            }
        }
    }
}