namespace PizzaMore.SignUp
{
    using System.Collections.Generic;
    using Utility;
    using Models;
    using Data;
    public class SignUp
    {
        private static IDictionary<string, string> RequestParameters;
        private static Header Header = new Header();
        public static void Main()
        {
            if (WebUtil.IsPost())
            {
                RegisterUser();
            }

            ShowPage();
        }

        private static void RegisterUser()
        {
            RequestParameters = WebUtil.RetrievePostParameters();
            string email = RequestParameters["email"];
            string password = RequestParameters["password"];

            User user = new User()
            {
                Email = email,
                Password = PasswordHasher.Hash(password)
            };

            PizzaMoreContext context = new PizzaMoreContext();

            using (context)
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
        }

        private static void ShowPage()
        {
            Header.Print();
            WebUtil.PrintFileContent("../www/pm/signup.html");
        }
    }
}
