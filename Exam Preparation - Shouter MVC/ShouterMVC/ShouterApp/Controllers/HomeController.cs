using ShouterApp.BindingModels;
using ShouterApp.Data;
using ShouterApp.Data.Contracts;
using ShouterApp.Models;
using ShouterApp.Services;
using SimpleHttpServer.Models;
using SimpleMVC.Attributes.Methods;
using SimpleMVC.Controllers;
using SimpleMVC.Interfaces;
using SimpleMVC.Interfaces.Generic;

namespace ShouterApp.Controllers
{
    public class HomeController : Controller
    {
        private IShouterContext context;

        public HomeController()
            : this(new ShouterContext())
        {

        }

        public HomeController(IShouterContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public IActionResult Feed(HttpResponse response, HttpSession session)
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterUserBindingModel model)
        {
            new UserService(this.context).RegisterUser(model);
            return View("Home", "Feed");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginUserBindingModel model, HttpResponse response, HttpSession session)
        {
            User user = new UserService(this.context).LoginUser(model, response, session);

            if (user != null)
            {
                new SessionService(this.context).AddUserSession(user, session);
                return View("Home", "FeedSigned");
            }

            return View();
        }
    }
}
