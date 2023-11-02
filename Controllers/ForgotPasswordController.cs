using Microsoft.AspNetCore.Mvc;
using MovieApp.Models;
using MovieApp.Services;

namespace MainProject.ForgotPassword.Controllers
{
    public class ForgotPasswordController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Message = TempData["Message"]?.ToString();
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
                TempData["Message"] = "Successfull.";
                return RedirectToAction("Index", "Login");
            }
            else
            {
                ViewBag.ErrorMessage = "Failed";
                return View("Index");
            }
        }
    }
}