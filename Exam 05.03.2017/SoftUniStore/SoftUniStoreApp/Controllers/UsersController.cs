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
using SoftUniStoreApp.Models;
using SoftUniStoreApp.Security;
using SoftUniStoreApp.Services;
using SoftUniStoreApp.ViewModels;

namespace SoftUniStoreApp.Controllers
{
    public class UsersController : Controller
    {

        [HttpGet]
        public IActionResult<IEnumerable<GameViewModel>> Home(HttpResponse response, HttpSession session)
        {
            User authenticatedUser = AuthenticationManager.GetAuthenticateduser(session.Id);

            if (authenticatedUser == null)
            {
                this.Redirect(response, "/home/login");
                return null;
            }

            IEnumerable<GameViewModel> games = new GameService().GetAllGames();

            return this.View(games);
        }
    }
}
