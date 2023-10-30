using Microsoft.AspNetCore.Mvc;
using MainProject.Login.Interface;
using MainProject.Login.Model;
using MongoDB.Bson.IO;
using Newtonsoft.Json;

namespace MainProject.Login.Controllers
{

    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        private readonly ILoginServices _loginServices;

        public LoginController(ILoginServices loginServices)
        {
            _loginServices = loginServices;
        }

        [HttpPost]
        public IActionResult Login(LoginModel request)
        {
            //if (ModelState.IsValid)
            //{
            bool result = _loginServices.LoginMethod(request);
            if (result)
            {
                return RedirectToAction("Index", "ThirdPartyApiData", new {data= request.EmailAddress});
            }
            //     }
            return Content("false");
        }
    }
}