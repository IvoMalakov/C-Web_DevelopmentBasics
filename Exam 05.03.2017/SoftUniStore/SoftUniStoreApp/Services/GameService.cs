using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SimpleHttpServer.Models;
using SimpleMVC.Interfaces;
using SimpleMVC.Interfaces.Generic;
using SoftUniStoreApp.BindingModels;
using SoftUniStoreApp.Models;
using SoftUniStoreApp.ViewModels;

namespace SoftUniStoreApp.Services
{
    public class GameService : Service
    {
        public IEnumerable<GameViewModel> GetAllGames()
        {
            var gamesVms = new List<GameViewModel>();

            var games = this.Context.Games.Select(g => new
            {
                g.Description,
                g.ImageThumbnail,
                g.Price,
                g.Size,
                g.Title,
                g.Id
            })
                //.Take(3)
                .ToList();

            foreach (var game in games)
            {
                GameViewModel model = new GameViewModel()
                {
                    Description = game.Description,
                    Id = game.Id,
                    ImageThumblain = game.ImageThumbnail,
                    Price = game.Price,
                    Size = game.Size,
                    Title = game.Title
                };

                gamesVms.Add(model);
            }

            return gamesVms;
        }

        public GameDetailsViewModel GetGameDetails(int id, HttpSession session)
        {
            Game game = this.Context.Games.Find(id);
            GameDetailsViewModel gameDetailsViewModel = Mapper.Map<GameDetailsViewModel>(game);
            return gameDetailsViewModel;
        }

        public void AddNewGame(AddNewGameBindingModel model)
        {
            Game game = Mapper.Map<Game>(model);

            this.Context.Games.Add(game);
            this.Context.SaveChanges();
        }

        public void DeleteGame(int id)
        {
            Game gameForDelete = this.Context.Games.Find(id);
            this.Context.Games.Remove(gameForDelete);
            this.Context.SaveChanges();
        }

        public EditGameViewModel GetEditGame(int id)
        {
            Game game = this.Context.Games.Find(id);

            EditGameViewModel model = Mapper.Map<EditGameViewModel>(game);

            return model;
        }

        public void EditGame(EditGameBindingModel model)
        {
            Game game = this.Context.Games.Find(model.Description);
            if (game != null)
            {
                game.Title = model.Title;
            }

            this.Context.SaveChanges();
        }
    }
}
