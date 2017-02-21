namespace SimpleMVC.App.Views.Users
{
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SimpleMVC.App.MVC.Interfaces.Generic;
    using SimpleMVC.App.ViewModels;

    public class Greet : IRenderable<GreetViewModel>
    {
        public GreetViewModel Model { get; set; }

        public string Render()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"<p>Hello user with session id: {this.Model.SessionId}</p>");
            return sb.ToString();
        }
    }
}