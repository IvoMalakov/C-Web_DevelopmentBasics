using ShouterApp.Data.Contracts;
using ShouterApp.Data.Repositories;

namespace ShouterApp.Data
{
    public class ShouterData
    {
        private readonly IShouterContext context;

        public ShouterData() 
            : this(new ShouterContext())
        {
            
        }

        public ShouterData(IShouterContext context)
        {
            this.context = context;
        }

        public UserRepository UserRepository => new UserRepository(this.context);

        public LoginRepository LoginRepository => new LoginRepository(this.context);

        public IShouterContext Context => this.context;

        public int SaveChanges() => this.context.SaveChanges();
    }
}
