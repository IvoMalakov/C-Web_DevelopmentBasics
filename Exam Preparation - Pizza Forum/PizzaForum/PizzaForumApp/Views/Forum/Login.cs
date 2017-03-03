using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaForumApp.Helpers;
using SimpleMVC.Interfaces;

namespace PizzaForumApp.Views.Forum
{
    public class Login : IRenderable
    {
        public string Render()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(HtmlReader.Read(Constans.Header));
            sb.AppendLine(HtmlReader.Read(Constans.NavNotLogged));
            sb.AppendLine(HtmlReader.Read(Constans.Login));
            sb.AppendLine(HtmlReader.Read(Constans.Footer));

            return sb.ToString();
        }
    }
}
