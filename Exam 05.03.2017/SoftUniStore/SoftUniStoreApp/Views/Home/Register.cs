using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleMVC.Interfaces;
using SoftUniStoreApp.Helpers;

namespace SoftUniStoreApp.Views.Home
{
    public class Register : IRenderable
    {
        public string Render()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(HtmlReader.Read(Constans.Header));
            sb.AppendLine(HtmlReader.Read(Constans.NavNotLogged));
            sb.AppendLine(HtmlReader.Read(Constans.Register));
            sb.AppendLine(HtmlReader.Read(Constans.Footer));

            return sb.ToString();
        }
    }
}
