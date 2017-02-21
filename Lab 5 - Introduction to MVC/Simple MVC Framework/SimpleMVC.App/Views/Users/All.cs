namespace SimpleMVC.App.Views.Users
{
    using System.Collections.Generic;
    using System.Text;
    using MVC.Interfaces.Generic;
    using ViewModels;

    public class All : IRenderable<IEnumerable<AllUsersViewModel>>
    {

        public string Render()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<a href=\"../../home/index\">&lt; Home</a>");
            sb.AppendLine("<h3>All users</h3>");
            sb.AppendLine("<ul>");
            int idCount = 1;

            foreach (var userName in ((IRenderable<IEnumerable<AllUsersViewModel>>)this).Model)
            {
                sb.AppendLine($"<li><a href=\"profile?id={idCount}\">{userName.Username}</a></li>");
                idCount++;
            }
            sb.AppendLine("</ul>");

            return sb.ToString();
        }

        IEnumerable<AllUsersViewModel> IRenderable<IEnumerable<AllUsersViewModel>>.Model { get; set; }
    }
}
