namespace SimpleMVC.App.Views.Users
{
    using System.Text;
    using SimpleMVC.App.MVC.Interfaces;

    public class Logout : IReanderable
    {
        public string Render()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<h2>Notes app</h2>");
            sb.AppendLine("<a href=\"/users/all\">All users</a>");
            sb.AppendLine("<a href=\"/users/register\">Register user</a>");
            sb.AppendLine("<a href=\"/users/logout\"Log Out</a>");

            return sb.ToString();
        }
    }
}
