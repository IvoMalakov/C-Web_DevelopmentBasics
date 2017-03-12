using SoftUniStoreApp.Data;

namespace SoftUniStoreApp.Services
{
    public abstract class Service
    {
        protected Service()
        {
            this.Context = Data.Data.Context;
        }

        public SoftUniStoreContext Context { get; set; }
    }
}
