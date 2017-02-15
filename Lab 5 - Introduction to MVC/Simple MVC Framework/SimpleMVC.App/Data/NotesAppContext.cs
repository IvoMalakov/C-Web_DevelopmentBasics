namespace SimpleMVC.App.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Models;

    public class NotesAppContext : DbContext
    {

        public NotesAppContext()
            : base("name=NotesAppContext")
        {
        }

        public virtual DbSet<User> Users { get; set; } 

        public virtual DbSet<Note> Notes { get; set; }

    }
}