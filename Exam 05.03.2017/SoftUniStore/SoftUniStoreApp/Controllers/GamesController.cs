using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleHttpServer.Models;
using SimpleMVC.Attributes.Methods;
using SimpleMVC.Controllers;
using SimpleMVC.Interfaces;
using SimpleMVC.Interfaces.Generic;
using SoftUniStoreApp.BindingModels;
using SoftUniStoreApp.Models;
using SoftUniStoreApp.Security;
using SoftUniStoreApp.Services;
using SoftUniStoreApp.ViewModels;

namespace SoftUniStoreApp.Controllers
{
    public class GamesController : Controller
    {
        [HttpGet]
        public IActionResult<GameDetailsViewModel> Details(int id, HttpSession session, HttpResponse response)
        {
            User authenticatedUser = AuthenticationManager.GetAuthenticateduser(session.Id);

            if (authenticatedUser == null)
            {
                this.Redirect(response, "/home/login");
                return null;
            }

            return this.View(new GameService().GetGameDetails(id, session));
        }

        [HttpGet]
        public IActionResult Add(HttpResponse response, HttpSession session)
        {
            User authenticatedUser = AuthenticationManager.GetAuthenticateduser(session.Id);

            if (authenticatedUser == null)
            {
                this.Redirect(response, "/home/login");
                return null;
            }

            return this.View();
        }

        [HttpPost]
        public IActionResult Add(HttpResponse response, HttpSession session, AddNewGameBindingModel model)
        {
            new GameService().AddNewGame(model);
            this.Redirect(response, "/users/home");
            return null;
        }

        [HttpGet]
        public IActionResult<DeleteGameBindingModel> Delete(HttpResponse response, HttpSession session, int id, DeleteGameBindingModel model)
        {
            User authenticatedUser = AuthenticationManager.GetAuthenticateduser(session.Id);

            if (authenticatedUser == null)
            {
                this.Redirect(response, "/home/login");
                return null;
            }

            return this.View(model);
        }

        [HttpPost]
        public IActionResult Delete(HttpResponse response, HttpSession session, int id)
        {
            User authenticatedUser = AuthenticationManager.GetAuthenticateduser(session.Id);

            if (authenticatedUser == null)
            {
                this.Redirect(response, "/home/login");
                return null;
            }

            new GameService().DeleteGame(id);
            this.Redirect(response, "/games/all");
            return null;
        }

        [HttpGet]
        public IActionResult<EditGameViewModel> Edit(HttpResponse response, HttpSession session, int id)
        {
            User authenticatedUser = AuthenticationManager.GetAuthenticateduser(session.Id);

            if (authenticatedUser == null)
            {
                this.Redirect(response, "/home/login");
                return null;
            }

            EditGameViewModel viewModel = new GameService().GetEditGame(id);

            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(HttpResponse response, HttpSession session, EditGameBindingModel model)
        {
            User authenticatedUser = AuthenticationManager.GetAuthenticateduser(session.Id);

            if (authenticatedUser == null)
            {
                this.Redirect(response, "/home/login");
                return null;
            }

            new GameService().EditGame(model);
            this.Redirect(response, "/users/home");
            return null;
        }
    }
}
