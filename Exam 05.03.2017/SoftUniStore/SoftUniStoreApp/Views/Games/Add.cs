using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleMVC.Interfaces;
using SoftUniStoreApp.Helpers;

namespace SoftUniStoreApp.Views.Games
{
    public class Add : IRenderable
    {
        public string Render()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(HtmlReader.Read(Constans.Header));
            sb.AppendLine(HtmlReader.Read(Constans.NavLogged));
            sb.AppendLine(HtmlReader.Read(Constans.AddGame));
            sb.AppendLine(HtmlReader.Read(Constans.Footer));

            return sb.ToString();
        }
    }
}
