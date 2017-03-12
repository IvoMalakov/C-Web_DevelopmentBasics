using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleHttpServer;
using SimpleMVC;
using AutoMapper;
using SoftUniStoreApp.BindingModels;
using SoftUniStoreApp.Models;
using SoftUniStoreApp.ViewModels;

namespace SoftUniStoreApp
{
    class SoftUniAppStart
    {
        static void Main()
        {
            ConfigureAutomapper();
            HttpServer server = new HttpServer(8081, RouterTable.Routes);
            MvcEngine.Run(server, "SoftUniStoreApp");
        }

        private static void ConfigureAutomapper()
        {
            Mapper.Initialize(expression => expression.CreateMap<RegisterUserBindingModel, User>());
            Mapper.Initialize(expression => expression.CreateMap<GameViewModel, Game>());
            Mapper.Initialize(expression => expression.CreateMap<Game, GameDetailsViewModel>());
            Mapper.Initialize(expression => expression.CreateMap<AddNewGameBindingModel, Game>());
            Mapper.Initialize(expression => expression.CreateMap<EditGameViewModel, Game>());
        }
    }
}
