using SimpleHttpServer.Models;

namespace PizzaMore.Controllers
{
    using SimpleMVC.Interfaces;
    using SimpleMVC.Controllers;
    using SimpleMVC.Attributes.Methods;
    using PizzaMore.Services;
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string request)
        {
            return View("Home", request);
        }
    }
}
