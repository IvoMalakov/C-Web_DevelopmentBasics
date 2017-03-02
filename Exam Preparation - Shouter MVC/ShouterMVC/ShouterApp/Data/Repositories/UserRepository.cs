using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShouterApp.Data.Contracts;
using ShouterApp.Models;

namespace ShouterApp.Data.Repositories
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(IShouterContext context) : base(context)
        {
        }

        public User FindUserByUsername(string username)
        {
            return this.EntityTable.FirstOrDefault(u => u.Username == username);
        }

        public void AddOrUpdateUser(User user)
        {
            this.EntityTable.AddOrUpdate(user);
        }
    }
}
