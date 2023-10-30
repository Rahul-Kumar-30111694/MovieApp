using Microsoft.AspNetCore.Mvc;
using MovieApp.Models;
using MovieApp.Services;

namespace MovieApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }
        public IActionResult Admin()
        {
            return View();
        }

        public IActionResult AdminControl(MoviesInDB request)
        {
            if(_adminService.AdminMethods(request))
            {
                return Ok("Success");
            }
            else
            {
                return Ok("Failed");
            }
        }
    }
}