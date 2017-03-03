using ShouterApp.BindingModels;
using ShouterApp.Data;
using ShouterApp.Data.Contracts;
using ShouterApp.Models;
using ShouterApp.Security;
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
        private SignInManager signInManager;

        public HomeController()
            : this(new ShouterContext())
        {

        }

        public HomeController(IShouterContext context)
        {
            this.context = context;
            this.signInManager = new SignInManager(this.context);
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
        public IActionResult Login(HttpResponse response, HttpSession session, LoginUserBindingModel model)
        {
            User user = new UserService(this.context).LoginUser(model, response, session);

            if (user != null)
            {
                new SessionService(this.context).AddUserSession(user, session);
                return View("Home", "Feedsigned");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Feedsigned(HttpResponse response, HttpSession session)
        {
            if (!signInManager.IsAuthenticated(session))
            {
                Redirect(response, "/home/login");
                return null;
            }

            return View();
        }

        [HttpGet]
        public IActionResult Logout(HttpResponse response, HttpSession session)
        {
            Login login = new UserService(this.context).LogoutUser(response, session);
            new SessionService(this.context).DeleteUserSession(login, session.Id, response);
            Redirect(response, "/home/feed");
            return null;
        }
    }
}
