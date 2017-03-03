using System.Text;
using PizzaForumApp.Helpers;
using SimpleMVC.Interfaces;

namespace PizzaForumApp.Views.Home
{
    public class Index : IRenderable
    {
        public string Render()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(HtmlReader.Read(Constans.Header));
            sb.AppendLine(HtmlReader.Read(Constans.NavNotLogged));
            sb.AppendLine(HtmlReader.Read(Constans.Index));
            sb.AppendLine(HtmlReader.Read(Constans.Footer));

            return sb.ToString();
        }
    }
}
