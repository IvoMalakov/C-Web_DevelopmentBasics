namespace PizzaMore.Data
{
    public class Data
    {
        private static PizzaMoreContext context;

        public static PizzaMoreContext Context => context ?? (context = new PizzaMoreContext());
    }
}