namespace PizzaMore.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using PizzaMore.Data.Models;

    public class PizzaMoreContext : DbContext
    {

        public PizzaMoreContext()
            : base("name=PizzaMoreContext")
        {
        }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Pizza> Pizzas { get; set; }

        public virtual DbSet<Session> Sessions { get; set; }
    }
}