using System;
using System.Data.Entity.Core.Metadata.Edm;
using PizzaForumApp.BindingModels;
using PizzaForumApp.Services;
using SimpleHttpServer.Models;
using SimpleMVC.Attributes.Methods;
using SimpleMVC.Controllers;
using SimpleMVC.Interfaces;

namespace PizzaForumApp.Controllers
{
    public class ForumController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Register(HttpResponse response, RegisterUserBindingModel model)
        {
            bool isRegisterUserSuccessful = new ForumService(Data.Data.Context).RegisterUser(model);

            if (isRegisterUserSuccessful)
            {
                Redirect(response, "/forum/login");
                return null;
            }

            Redirect(response, "/forum/register");
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
            bool isLoginSuccessfull = new ForumService(Data.Data.Context).LoginUser(model, session);

            if (isLoginSuccessfull)
            {
                Redirect(response, "/home/topics");
                return null;
            }

            Redirect(response, "/forum/login");
            return null;
        }

        [HttpGet]
        public IActionResult Logout(HttpResponse response, HttpSession session)
        {
            new ForumService(Data.Data.Context).LogoutUser(response, session.Id);
            Redirect(response, "/home/index");
            return null;
        }
    }
}
