namespace PizzaMore.SignIn
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models;
    using Data;
    using Utility;
    public class SignIn
    {
        private static IDictionary<string, string> RequestParameters;
        private static Header Header = new Header();
        public static void Main()
        {
            if (WebUtil.IsPost())
            {
                LogIn();
            }

            ShowPage();
        }

        private static void LogIn()
        {
            RequestParameters = WebUtil.RetrievePostParameters();
            string email = RequestParameters["email"];
            string password = RequestParameters["password"];
            string hashedPassword = PasswordHasher.Hash(password);

            PizzaMoreContext context = new PizzaMoreContext();
            Random rnd = new Random();

            using (context)
            {
                User user = context.Users.SingleOrDefault(u => u.Email == email);

                if (user.Password == hashedPassword)
                {
                    Session session = new Session()
                    {
                        Id = rnd.Next().ToString(),
                        User = user
                    };

                    if (user != null)
                    {
                        Header.AddCoockie(new Cookie(Constants.SessionIdKey, session.Id));
                    }

                    context.Sessions.Add(session);
                    context.SaveChanges();
                }
            }

            Console.WriteLine("Location: menu.exe\n\n");
        }

        private static void ShowPage()
        {
            Header.Print();
            WebUtil.PrintFileContent("../www/pm/signin.html");
        }
    }
}
