using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleMVC.Interfaces.Generic;
using SoftUniStoreApp.BindingModels;
using SoftUniStoreApp.Helpers;

namespace SoftUniStoreApp.Views.Games
{
    public class Delete : IRenderable<DeleteGameBindingModel>
    {
        public string Render()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(HtmlReader.Read(Constans.Header));
            sb.AppendLine(HtmlReader.Read(Constans.NavLogged));
            sb.AppendLine(HtmlReader.Read(Constans.DeleteGame));
            sb.AppendLine(HtmlReader.Read(Constans.Footer));

            return sb.ToString();
        }

        public DeleteGameBindingModel Model { get; set; }
    }
}
