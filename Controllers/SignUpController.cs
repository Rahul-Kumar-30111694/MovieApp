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
                if (_signUpService.SignUpMethod(request))
                {
                    TempData["Message"] = "Successful Registration.";
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    TempData["Message"] = "Wrong inputs. Please Enter All Details in Valid Formats.";
                    return View("Index");
                }
            }
            else
            {
                TempData["Message"] = "Registration Failed.";
                return View("Index");
            }
        }
    }
}