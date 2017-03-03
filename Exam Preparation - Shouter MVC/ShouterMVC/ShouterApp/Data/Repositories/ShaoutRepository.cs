using ShouterApp.Data.Contracts;
using ShouterApp.Models;

namespace ShouterApp.Data.Repositories
{
    public class ShaoutRepository : Repository<Shaout>
    {
        public ShaoutRepository(IShouterContext context) : base(context)
        {
        }
    }
}
