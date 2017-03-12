using SoftUniStoreApp.Models;

namespace SoftUniStoreApp.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class SoftUniStoreContext : DbContext
    {
       
        public SoftUniStoreContext()
            : base("name=SoftUniStoreContext")
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Login> Logins { get; set; }

        public DbSet<Game> Games { get; set; }
    }
}