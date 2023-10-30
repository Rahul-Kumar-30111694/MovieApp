using Microsoft.AspNetCore.Mvc;
using MovieApp.Models;
using MovieApp.Services;

namespace MainProject.SignUp.Controllers
{
    public class SignUpController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly ISignUpService _signUpService;

        public SignUpController(ISignUpService signUpService)
        {
            _signUpService = signUpService;
        }

        [HttpPost]
        public IActionResult SignUp(SignUpModel request)
        {
            if (ModelState.IsValid)
            {
                _signUpService.SignUpMethod(request);
                return Content("Successful Registration.");
            }
            else
            {
                return Content("Registration Failed.");
            }
        }
    }
}