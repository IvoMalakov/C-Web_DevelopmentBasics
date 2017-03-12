using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleMVC.Interfaces;
using SimpleMVC.Interfaces.Generic;
using SoftUniStoreApp.Helpers;
using SoftUniStoreApp.ViewModels;

namespace SoftUniStoreApp.Views.Users
{
    public class Home : IRenderable<IEnumerable<GameViewModel>>
    {
        public string Render()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(HtmlReader.Read(Constans.Header));
            sb.AppendLine(HtmlReader.Read(Constans.NavLogged));
            sb.AppendLine(HtmlReader.Read(Constans.Home));
            int count = 1;

            foreach (var model in this.Model)
            {
                if (count == 3)
                {
                    sb.AppendLine("</div>");
                    count = 1;
                }
                sb.AppendLine("<div class=\"row\"");
                sb.Append(model.ToString());
                count++;
            }

            sb.AppendLine(Environment.NewLine);

            sb.AppendLine(HtmlReader.Read(Constans.HomeEnd));
            sb.AppendLine(HtmlReader.Read(Constans.Footer));

            return sb.ToString();
        }

        public IEnumerable<GameViewModel> Model { get; set; }
    }
}
