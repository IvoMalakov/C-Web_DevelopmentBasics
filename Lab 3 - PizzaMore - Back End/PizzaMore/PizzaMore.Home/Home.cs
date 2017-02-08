namespace PizzaMore.Home
{
    using Utility;
    using Data;
    using Models;
    using System.Collections.Generic;

    internal class Home
    {
        private static IDictionary<string, string> RequestParameters;
        private static Session Session;
        private static Header Header = new Header();
        private static string Language;

        public static void Main()
        {
            AddDefaultLanguageCoockie();

            if (WebUtil.IsGet())
            {
                RequestParameters = WebUtil.RetrieveGetParameters();
                TryLogOut();
                Language = WebUtil.GetCookies()["lang"].Value;
            }

            else if (WebUtil.IsPost())
            {
                RequestParameters = WebUtil.RetrievePostParameters();
                Header.AddCoockie(new Cookie("lang", RequestParameters["language"]));
                Language = RequestParameters["language"];
            }

            ShowPage();
        }

        private static void TryLogOut()
        {
            if (RequestParameters.ContainsKey("logout"))
            {
                if (RequestParameters["logout"] == "true")
                {
                    Session = WebUtil.GetSession();
                    PizzaMoreContext context = new PizzaMoreContext();

                    using (context)
                    {
                        Session session = context.Sessions.Find(Session.Id);
                        context.Sessions.Remove(session);
                        context.SaveChanges();
                    }
                }
            }
        }

        private static void AddDefaultLanguageCoockie()
        {
            if (!WebUtil.GetCookies().ContainsKey("lang"))
            {
                Header.AddCoockie(new Cookie("lang", "EN"));
                Language = "EN";
                ShowPage();
            }
        }

        private static void ShowPage()
        {
            Header.Print();

            if (Language.Equals("DE"))
            {
                ServerHtmlDe();
            }

            else
            {
                ServerHtmlEn();
            }
        }

        private static void ServerHtmlDe()
        {
            string path = "../www/pm/home-de.html";
            WebUtil.PrintFileContent(path);
        }

        private static void ServerHtmlEn()
        {
            string path = "../www/pm/home.html";
            WebUtil.PrintFileContent(path);
        }
    }
}
