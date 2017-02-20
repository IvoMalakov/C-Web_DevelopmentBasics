namespace SimpleMVC.App.MVC.Security
{
    using System.Linq;
    using SimpleHttpServer.Models;
    using SimpleMVC.App.MVC.Interfaces;
    using SimpleMVC.App.Data.Models;

    public class SignInManager
    {
        private IDbIdentityContext dbcContext;

        public SignInManager(IDbIdentityContext context)
        {
            this.dbcContext = context;
        }

        public bool IsAuthenticated(HttpSession session)
        {
            var login = this.dbcContext.Logins.FirstOrDefault(l => l.SessionId == session.Id && l.IsActive);

            if (login != null)
            {
                return true;
            }

            return false;
        }
    }
}
