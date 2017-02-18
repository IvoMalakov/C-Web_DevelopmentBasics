namespace SimpleMVC.App.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using SimpleMVC.App.MVC.Interfaces;
    using Models;

    public class NotesAppContext : DbContext, IDbIdentityContext
    {

        public NotesAppContext()
            : base("name=NotesAppContext")
        {
        }

        public virtual DbSet<Login> Logins { get; set; }

        public virtual DbSet<User> Users { get; set; }
        public void Save()
        {
            this.SaveChanges();
        }

        public virtual DbSet<Note> Notes { get; set; }

    }
}