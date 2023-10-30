using MainProject.SignUp.Interface;
using MainProject.SignUp.Model;
using Microsoft.AspNetCore.Mvc;

namespace MainProject.SignUp.Controllers
{
    public class SignUpController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly ISignUpServices _signUpServices;

        public SignUpController(ISignUpServices signUpServices)
        {
            _signUpServices = signUpServices;
        }

        [HttpPost]
        public IActionResult SignUp(SignUpModel request)
        {
            if (ModelState.IsValid)
            {
                _signUpServices.SignUpMethod(request);
                return Content("Successful Registration.");
            }
            else
            {
                return Content("Registration Failed.");
            }
        }
    }
}