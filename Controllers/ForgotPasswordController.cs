using Microsoft.AspNetCore.Mvc;
using MovieApp.Models;
using MovieApp.Services;

namespace MainProject.ForgotPassword.Controllers
{
    public class ForgotPasswordController : Controller
    {
        public IActionResult Index()
        {
            return View();
        } 

        private readonly IForgotPasswordService _forgotPasswordService;

        public ForgotPasswordController(IForgotPasswordService forgotPasswordService)
        {
            _forgotPasswordService = forgotPasswordService;
        }

        [HttpPost]
        public IActionResult ForgotPassword(ForgotPasswordModel request)
        {
            if(ModelState.IsValid)
            {
                _forgotPasswordService.ForgotPasswordMethod(request);
                return Content("Success.");
            }
            return Content("Unsuccess.");
        }
    }
}