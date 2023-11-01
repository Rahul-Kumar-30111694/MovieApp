using Microsoft.AspNetCore.Mvc;
using MovieApp.Models;
using MovieApp.Services;

namespace MainProject.Login.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }
        public IActionResult Index()
        {
            ViewBag.Message = TempData["Message"] as string;
            return View();
        }
        public IActionResult Logout()
        {
            Response.Cookies.Delete("Token");
            return RedirectToAction("Index");
        }
        public IActionResult Login(LoginModel request)
        {
            if (ModelState.IsValid)
            {
                if (_loginService.LoginMethod(request))
                {
                    return RedirectToAction("HomePage", "Homepage", new { data = request.EmailAddress });
                }
                else
                {
                    ViewBag.Message = "Wrong Credentials. Please try again.";
                    return View("Index");
                }
            }
            else
            {
                ViewBag.Message = "Invalid User. Please try again.";
                return View("Index");
            }
        }
    }
}