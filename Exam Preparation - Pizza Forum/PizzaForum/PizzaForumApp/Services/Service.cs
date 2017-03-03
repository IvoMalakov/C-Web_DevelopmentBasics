using PizzaForumApp.Data;

namespace PizzaForumApp.Services
{
    public abstract class Service
    {
        protected Service(PizzaForumContext context)
        {
            this.Context = Data.Data.Context;
        }

        public PizzaForumContext Context { get; set; }
    }
}
