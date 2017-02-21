namespace SimpleMVC.App.Controllers
{
    using MVC.Interfaces;
    using SimpleMVC.App.MVC.Controllers;
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
