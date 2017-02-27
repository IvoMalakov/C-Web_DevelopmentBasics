namespace PizzaMore.Views.Menu
{
    using PizzaMore.Utilities;
    using SimpleMVC.Interfaces;

    public class Add : IRenderable
    {
        public string Render()
        {
            string content = WebUtils.RetriveContentPathFolder(Constants.AddPizza);
            return content;
        }
    }
}
