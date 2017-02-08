namespace PizzaMore.YourSuggestions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models;
    using Data;
    using Utility;
    public class YourSuggestions
    {
        private static Session Session;
        private static IDictionary<string, string> RequestParams;
        private static Header Header = new Header();

        public static void Main()
        {
            Session = WebUtil.GetSession();

            if (Session == null)
            {
                Header.Print();
                WebUtil.PageNotAllowed();
                return;
            }

            if (WebUtil.IsGet())
            {
                ShowPage();
            }
            else if (WebUtil.IsPost())
            {
                DeletePizza();
                ShowPage();
            }
        }

        private static void DeletePizza()
        {
            RequestParams = WebUtil.RetrievePostParameters();
            PizzaMoreContext context = new PizzaMoreContext();

            using (context)
            {
                Pizza pizza = context.PizzaSuggestions.Find(RequestParams["pizzaId"]);
                context.PizzaSuggestions.Remove(pizza);
                context.SaveChanges();
            }
        }

        private static void ShowPage()
        {
            Header.Print();
            WebUtil.PrintFileContent("../www/pm/yoursuggestions-top.html");
            PrintListOfSuggestedItems();
            WebUtil.PrintFileContent("../www/pm/yoursuggestions-bottom.html");
        }

        private static void PrintListOfSuggestedItems()
        {
            PizzaMoreContext context = new PizzaMoreContext();

            using (context)
            {
                var suggestions = context.PizzaSuggestions.Where(p => p.OwnerId == Session.UserId);

                Console.WriteLine("<ul>");
                foreach (var suggestion in suggestions)
                {
                    Console.WriteLine("<form method=\"POST\">");
                    Console.WriteLine($"<li><a href=\"DetailsPizza.exe?pizzaid={suggestion.Id}\">{suggestion.Title}</a> <input type=\"hidden\" name=\"pizzaId\" value=\"{suggestion.Id}\"/> <input type=\"submit\" class=\"btn btn-sm btn-danger\" value=\"X\"/></li>");
                    Console.WriteLine("</form>");
                }
                Console.WriteLine("</ul>");
            }
        }
    }
}
