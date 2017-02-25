namespace PizzaMore.Controllers
{
    using PizzaMore.Models;
    using SimpleHttpServer.Models;
    using PizzaMore.Services.DataBaseServices;
    using PizzaMore.BindingModels;
    using PizzaMore.Security;
    using SimpleMVC.Controllers;
    using SimpleMVC.Attributes.Methods;
    using SimpleMVC.Interfaces;
    public class UsersController : Controller
    {
        private SignInManager signInManager;

        public UsersController()
        {
            this.signInManager = new SignInManager(Data.Data.Context);
        }

        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Signup(UserBindingModel model)
        {
            UserService service = new UserService(Data.Data.Context);
            service.RegisterUser(model);
            return View("Home", "Index");
        }

        [HttpGet]
        public IActionResult Signin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Signin(UserBindingModel model, HttpSession session)
        {
            UserService service = new UserService(Data.Data.Context);
            User user = service.LoginUser(model, session);

            if (user != null)
            {
                SessionService sessionService = new SessionService(Data.Data.Context);
                sessionService.AddUserSession(user, session);
                return View("Home", "IndexLogged");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Logout(HttpSession session)
        {
            this.signInManager.LogOut(session);
            return View("Home", "Index");
        }
    }
}
