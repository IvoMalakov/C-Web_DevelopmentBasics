using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleMVC.Interfaces.Generic;
using SoftUniStoreApp.ViewModels;

namespace SoftUniStoreApp.Views.Games
{
    public class Details : IRenderable<GameDetailsViewModel>
    {
        public string Render()
        {
            return this.Model.ToString();
        }

        public GameDetailsViewModel Model { get; set; }
    }
}
