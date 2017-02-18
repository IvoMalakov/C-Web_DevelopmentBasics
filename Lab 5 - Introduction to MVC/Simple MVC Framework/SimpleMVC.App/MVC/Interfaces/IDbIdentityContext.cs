namespace SimpleMVC.App.MVC.Interfaces
{
    using System.Data.Entity;
    using SimpleMVC.App.Data.Models;

    public interface IDbIdentityContext
    {
        DbSet<Login> Logins { get; }

        DbSet<User> Users { get; }

        void Save();
    }
}
