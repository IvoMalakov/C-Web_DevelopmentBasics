namespace PizzaMore.Views.Users
{
    using PizzaMore.Utilities;
    using SimpleMVC.Interfaces;

    public class Signup : IRenderable
    {
        public string Render()
        {
            string content = WebUtils.RetriveContentPathFolder(Constants.SignUp);
            return content;
        }
    }
}