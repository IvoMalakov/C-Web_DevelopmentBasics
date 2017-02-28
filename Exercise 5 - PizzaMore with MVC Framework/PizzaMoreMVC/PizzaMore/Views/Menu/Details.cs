namespace PizzaMore.Views.Menu
{
    using PizzaMore.Utilities;
    using PizzaMore.ViewModels;
    using SimpleMVC.Interfaces.Generic;

    public class Details : IRenderable<PizzaDetailsViewModel>
    {
        public string Render()
        {
            string html = WebUtils.RetriveContentPathFolder(Constants.Details);
            html = html
                .Replace("{pizza.Title}", this.Model.Title)
                .Replace("{pizza.ImageUrl}", this.Model.ImageUrl)
                .Replace("{pizza.Recipe}", this.Model.Recipe)
                .Replace("{pizza.UpVote}", this.Model.UpVotes.ToString())
                .Replace("{pizza.DownVote}", this.Model.DownVotes.ToString());

            return html;
        }

        public PizzaDetailsViewModel Model { get; set; }
    }
}
