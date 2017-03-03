using ShouterApp.Utilities;
using SimpleMVC.Interfaces;

namespace ShouterApp.Views.Home
{
    public class Feedsigned : IRenderable
    {
        public string Render()
        {
            string html = HtmlReader.Read(Constans.FeedSigned);
            return html;
        }
    }
}
