using System.Text;
using PizzaForumApp.Helpers;
using SimpleMVC.Interfaces;

namespace PizzaForumApp.Views.Forum
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
