using MainProject.ForgotPassword.Interface;
using MainProject.ForgotPassword.Model;
using Microsoft.AspNetCore.Mvc;

namespace MainProject.ForgotPassword.Controllers
{
    public class ForgotPasswordController : Controller
    {
        public IActionResult Index()
        {
            return View();
        } 

        private readonly IForgotPasswordServices _forgotPasswordServices;

        public ForgotPasswordController(IForgotPasswordServices forgotPasswordServices)
        {
            _forgotPasswordServices = forgotPasswordServices;
        }

        [HttpPost]
        public IActionResult ForgotPassword(ForgotPasswordModel request)
        {
            if(ModelState.IsValid)
            {
                _forgotPasswordServices.ForgotPasswordMethod(request);
                return Content("Success.");
            }
            return Content("Unsuccess.");
        }
    }
}