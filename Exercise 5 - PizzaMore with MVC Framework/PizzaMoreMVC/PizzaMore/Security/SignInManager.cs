namespace PizzaMore.Security
{
    using System;
    using System.Linq;
    using PizzaMore.Data;
    using PizzaMore.Models;
    using SimpleHttpServer.Models;
    public class SignInManager
    {
        public PizzaMoreContext dbContext;

        public SignInManager(PizzaMoreContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public bool IsAuthenticated(HttpSession session)
        {
            bool isAuthenticated = dbContext.Sessions.Any(s => s.Id == session.Id);
            return isAuthenticated;
        }


        public void LogOut(HttpSession session)
        {
            Session sessionEntity = dbContext.Sessions.FirstOrDefault(s => s.Id == session.Id);

            if (sessionEntity != null)
            {
                session.Id = new Random().Next().ToString();
                this.dbContext.Sessions.Remove(sessionEntity);
                this.dbContext.SaveChanges();
            }
        }
    }
}

