namespace PizzaMore.Views.Home
{
    using PizzaMore.Utilities;
    using SimpleMVC.Interfaces;

    public class IndexLogged : IRenderable
    {
        public string Render()
        {
            string content = WebUtils.RetriveContentPathFolder(Constants.HomeIndexLogged);
            return content;
        }
    }
}