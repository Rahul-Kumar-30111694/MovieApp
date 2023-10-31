using Microsoft.AspNetCore.Mvc;
using MovieApp.Methods;
using MovieApp.Models;
using MovieApp.Services;

namespace MainProject.Login.Controllers
{

    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;
        private readonly IJWTMethod _jWTMethod;

        public LoginController(ILoginService loginService, IJWTMethod jWTMethod)
        {
            _loginService = loginService;
            _jWTMethod = jWTMethod;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Logout()
        {
            Response.Cookies.Delete("Token");
            return RedirectToAction("Login");
        }
        public IActionResult Login(LoginModel request)
        {
            //if (ModelState.IsValid)
            //{
            bool result = _loginService.LoginMethod(request);
            if (result)
            {
                //TempData["Message"] = _jWTMethod.ValidateToken(_jWTMethod.CreateToken(request.EmailAddress));
                return RedirectToAction("HomePage", "Homepage", new { data = request.EmailAddress });
            }
            //     }
            return View("Index");
        }
    }
}