using ShouterApp.Data.Contracts;
using ShouterApp.Models;

namespace ShouterApp.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class ShouterContext : DbContext, IShouterContext
    {
        public ShouterContext()
            : base("name=ShouterContext")
        {
        }

        public IDbSet<User> Users { get; set; }

        public IDbSet<Login> Logins { get; set; }
        public DbContext DbContext => this;
        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}