using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleMVC.Interfaces.Generic;
using SoftUniStoreApp.Helpers;
using SoftUniStoreApp.ViewModels;

namespace SoftUniStoreApp.Views.Games
{
    public class Edit : IRenderable<EditGameViewModel.EditCategoryViewModel>
    {
        public string Render()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(HtmlReader.Read(Constans.Header));
            sb.AppendLine(HtmlReader.Read(Constans.NavLogged));
            sb.AppendLine(HtmlReader.Read(Constans.EditGame));
            sb.AppendLine(HtmlReader.Read(Constans.Footer));

            return sb.ToString();
        }

        public EditGameViewModel.EditCategoryViewModel Model { get; set; }
    }
}
