using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShouterApp.Data;
using ShouterApp.Data.Contracts;
using ShouterApp.Models;
using SimpleHttpServer.Models;

namespace ShouterApp.Services
{
    public class SessionService
    {
        private IShouterContext context;

        public SessionService()
            :this(new ShouterContext())
        {
            
        }

        public SessionService(IShouterContext context)
        {
            this.context = context;
        }

        public void AddUserSession(User user, HttpSession session)
        {
            Login login = new Login()
            {
                IsActive = true,
                SessionId = session.Id,
                User = user
            };

            this.context.Logins.Add(login);
            this.context.DbContext.SaveChanges();
        }
    }
}
