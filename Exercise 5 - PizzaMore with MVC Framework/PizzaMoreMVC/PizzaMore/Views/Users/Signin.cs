namespace PizzaMore.Views.Users
{
    using PizzaMore.Utilities;
    using SimpleMVC.Interfaces;

    public class Signin : IRenderable
    {
        public string Render()
        {
            string content = WebUtils.RetriveContentPathFolder(Constants.SignIn);
            return content;
        }
    }
}
