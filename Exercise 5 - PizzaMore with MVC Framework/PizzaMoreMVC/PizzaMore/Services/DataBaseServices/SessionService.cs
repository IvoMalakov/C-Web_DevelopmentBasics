using System;
using PizzaMore.Data;
using PizzaMore.Models;
using SimpleHttpServer.Models;

namespace PizzaMore.Services.DataBaseServices
{
    public class SessionService : DataBaseService
    {
        public SessionService(PizzaMoreContext context) : base(context)
        {
        }

        public void AddUserSession(User user, HttpSession session)
        {
            Session sessionEntity = new Session()
            {
                Id = session.Id,
                User = user
            };

            this.Context.Sessions.Add(sessionEntity);
            this.Context.SaveChanges();
        }
    }
}
