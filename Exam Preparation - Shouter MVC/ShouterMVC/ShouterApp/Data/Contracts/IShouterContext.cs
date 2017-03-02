using System.Data.Entity;
using ShouterApp.Models;

namespace ShouterApp.Data.Contracts
{
    public interface IShouterContext
    {
        IDbSet<User> Users { get; }

        IDbSet<Login> Logins { get; }

        DbContext DbContext { get; }

        int SaveChanges();

        IDbSet<T> Set<T>()
            where T : class;
    }
}
