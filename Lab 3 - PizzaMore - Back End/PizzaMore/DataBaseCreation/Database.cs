using System;

namespace DataBaseCreation
{
    using PizzaMore.Data;
    public class Database
    {
        static void Main()
        {
            PizzaMoreContext context = new PizzaMoreContext();
           context.Database.Initialize(true);
        }
    }
}
