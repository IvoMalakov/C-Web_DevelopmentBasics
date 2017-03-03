using System.Data.Entity;
using ShouterApp.Models;

namespace ShouterApp.Data.Contracts
{
    public interface IShouterContext
    {
        IDbSet<User> Users { get; }

        IDbSet<Login> Logins { get; }

        IDbSet<Notification> Notifications { get; }

        IDbSet<Shaout> Shaouts { get; }

        DbContext DbContext { get; }

        int SaveChanges();

        IDbSet<T> Set<T>()
            where T : class;
    }
}
