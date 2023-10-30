using Microsoft.AspNetCore.Mvc;
using MovieApp.Models;
using MovieApp.Services;

namespace MainProject.Login.Controllers
{

    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public IActionResult Login(LoginModel request)
        {
            //if (ModelState.IsValid)
            //{
            bool result = _loginService.LoginMethod(request);
            if (result)
            {
                return RedirectToAction("Index", "ThirdPartyApiData", new {data= request.EmailAddress});
            }
            //     }
            return Content("false");
        }
    }
}