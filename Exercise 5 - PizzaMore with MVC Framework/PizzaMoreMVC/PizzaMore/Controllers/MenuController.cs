namespace PizzaMore.Controllers
{
    using System.Collections.Generic;
    using SimpleMVC.Interfaces.Generic;
    using PizzaMore.Models;
    using SimpleHttpServer.Models;
    using PizzaMore.Services.DataBaseServices;
    using PizzaMore.BindingModels;
    using PizzaMore.ViewModels;
    using PizzaMore.Security;
    using SimpleMVC.Controllers;
    using SimpleMVC.Attributes.Methods;
    using SimpleMVC.Interfaces;

    public class MenuController : Controller
    {
        private SignInManager signInManager;

        public MenuController()
        {
            this.signInManager = new SignInManager(Data.Data.Context);
        }

        [HttpGet]
        public IActionResult<IEnumerable<PizzaViewModel>> Index(HttpSession session, HttpResponse response)
        {
            if (signInManager.IsAuthenticated(session))
            {
                return View(new PizzaService(Data.Data.Context).GetPizzas(session, "noString"));
            }

            Redirect(response, "/users/signin");
            return null;
        }

        [HttpPost]
        public IActionResult Index(PizzaVoteBindingModel model, HttpResponse response, HttpSession session)
        {
            new PizzaService(Data.Data.Context).VoteForPizza(model.Pizzaid, model.PizzaVote, session);

            Redirect(response, "/menu/index");
            return null;
        }

        [HttpGet]
        public IActionResult Add(HttpResponse response, HttpSession session)
        {
            if (!signInManager.IsAuthenticated(session))
            {
                Redirect(response, "/users/signin");
                return null;
            }

            return View();
        }

        [HttpPost]
        public IActionResult Add(PizzaBindingModel model, HttpSession session, HttpResponse response)
        {
            if (!signInManager.IsAuthenticated(session))
            {
                Redirect(response, "/users/signin");
                return null;
            }

            new PizzaService(Data.Data.Context).AddPizza(session, model);
            return View();
        }
    }
}
