namespace SimpleMVC.App.Views.Users
{
    using System.Text;
    using MVC.Interfaces.Generic;
    using ViewModels;

    public class All : IRenderable<AllUserNamesViewModel>
    {
        public AllUserNamesViewModel Model { get; set; }

        public string Render()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<a href=\"../../home/index\">&lt; Home</a>");
            sb.AppendLine("<h3>All users</h3>");
            sb.AppendLine("<ul>");
            int idCount = 1;

            foreach (var userName in Model.UserNames)
            {
                sb.AppendLine($"<li><a href=\"profile?id={idCount}\">{userName}</a></li>");
                idCount++;
            }
            sb.AppendLine("</ul>");

            return sb.ToString();
        }
    }
}
