namespace PizzaMore.Services.DataBaseServices
{
    using PizzaMore.Data;

    public abstract class DataBaseService
    {
        protected DataBaseService(PizzaMoreContext context)
        {
            this.Context = context;
        }

        public PizzaMoreContext Context { get; set; }
    }
}
