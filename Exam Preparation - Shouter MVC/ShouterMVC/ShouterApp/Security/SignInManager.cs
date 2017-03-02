using System.Linq;
using ShouterApp.Data.Contracts;
using SimpleHttpServer.Models;

namespace ShouterApp.Security
{
    public class SignInManager
    {
        private IShouterContext context;

        public SignInManager(IShouterContext context)
        {
            this.context = context;
        }

        public bool IsAuthenticated(HttpSession session)
        {
            return this.context.Logins.Any(l => l.IsActive && l.SessionId == session.Id);
        }
    }
}
