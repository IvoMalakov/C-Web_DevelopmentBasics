namespace PizzaMore.AddPizza
{
    using System;
    using System.Collections.Generic;
    using Models;
    using Data;
    using Utility;

    public class AddPizza
    {
        private static Session Session;
        private static IDictionary<string, string> PostParams;
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
                PostParams = WebUtil.RetrievePostParameters();
                PizzaMoreContext context = new PizzaMoreContext();

                using (context)
                {
                    User user = context.Users.Find(Session.UserId);
                    Pizza pizza = new Pizza()
                    {
                        Title = PostParams["title"],
                        Recipe = PostParams["recipe"],
                        ImageUrl = PostParams["url"],
                        DownVotes = 0,
                        UpVotes = 0,
                        OwnerId = user.Id
                    };

                    user.Suggestions.Add(pizza);
                    context.SaveChanges();
                }
            }
        }

        private static void ShowPage()
        {
            Header.Print();
            WebUtil.PrintFileContent("../www/pm/addpizza.html");
        }
    }
}
