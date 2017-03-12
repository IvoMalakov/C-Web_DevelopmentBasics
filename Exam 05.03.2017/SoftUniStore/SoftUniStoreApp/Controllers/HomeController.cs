using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleHttpServer.Models;
using SimpleMVC.Attributes.Methods;
using SimpleMVC.Controllers;
using SimpleMVC.Interfaces;
using SoftUniStoreApp.BindingModels;
using SoftUniStoreApp.Security;
using SoftUniStoreApp.Services;

namespace SoftUniStoreApp.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Register(HttpResponse response, RegisterUserBindingModel model)
        {
            bool isRegistrationSuccessfull = new HomeService().RegisterUser(model);

            if (!isRegistrationSuccessfull)
            {
                Redirect(response, "/home/register");
                return null;
            }

            Redirect(response, "/home/login");
            return null;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Login(HttpResponse response, HttpSession session, LoginUserBindingModel model)
        {
            bool isLoginSuccessfull = new HomeService().LoginUser(model, session);

            if (!isLoginSuccessfull)
            {
                Redirect(response, "/home/login");
                return null;
            }

            Redirect(response, "/users/home");
            return null;
        }

        [HttpGet]
        public IActionResult Logout(HttpResponse response, HttpSession session)
        {
            AuthenticationManager.LogoutUser(response, session.Id);
            this.Redirect(response, "/home/login");
            return null;
        }
    }
}
