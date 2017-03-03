using ShouterApp.Data.Contracts;
using ShouterApp.Models;

namespace ShouterApp.Data.Repositories
{
    public class NotificationRepository : Repository<Notification>
    {
        public NotificationRepository(IShouterContext context) : base(context)
        {
        }
    }
}
