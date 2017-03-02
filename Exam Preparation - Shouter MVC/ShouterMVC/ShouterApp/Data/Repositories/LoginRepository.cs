using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShouterApp.Data.Contracts;
using ShouterApp.Models;

namespace ShouterApp.Data.Repositories
{
    public class LoginRepository : Repository<Login>
    {
        public LoginRepository(IShouterContext context) : base(context)
        {

        }

        public User FindUserByLogin(string sessionId)
        {
            return this.EntityTable.FirstOrDefault(l => l.SessionId == sessionId).User;
        }

        public void CreateLogin(string sessionId, User user)
        {
            if (!this.EntityTable.Any(l => l.SessionId == sessionId && l.User.Username == user.Username))
            {
                Login login = new Login()
                {
                    User = user,
                    SessionId = sessionId,
                    IsActive = true
                };

                this.Insert(login);
            }
        }
    }
}
