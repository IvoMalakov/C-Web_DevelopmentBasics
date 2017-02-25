namespace PizzaMore.Views.Home
{
    using SimpleMVC.Interfaces;
    using PizzaMore.Utilities;

    public class Index : IRenderable
    {
        public string Render()
        {
            string htmlText = WebUtils.RetriveContentPathFolder(Constants.HomeEn);
            return htmlText;
        }
    }
}
