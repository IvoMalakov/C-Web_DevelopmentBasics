namespace PizzaMore.Utility
{
    using System;
    using System.Net;
    using System.Collections.Generic;
    using System.Linq;
    using System.IO;
    using Interfaces;
    using Models;
    using Data;

    public static class WebUtil
    {
        public static bool IsPost()
        {
            string enviromentVariable = Environment.GetEnvironmentVariable(Constants.RequestMethod);

            if (enviromentVariable != null)
            {
                string requestMethod = enviromentVariable.ToLower();

                if (requestMethod == "post")
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsGet()
        {
            string enviromentVariable = Environment.GetEnvironmentVariable(Constants.RequestMethod);

            if (enviromentVariable != null)
            {
                string requestMethod = enviromentVariable.ToLower();

                if (requestMethod == "get")
                {
                    return true;
                }
            }

            return false;
        }

        public static IDictionary<string, string> RetrieveGetParameters()
        {
            string parametreString = WebUtility.UrlDecode(Environment.GetEnvironmentVariable(Constants.QueryString));
            return RetrieveRequestParameters(parametreString);
        }

        public static IDictionary<string, string> RetrievePostParameters()
        {
            string parametreString = WebUtility.UrlDecode(Console.ReadLine());
            return RetrieveRequestParameters(parametreString);
        }

        private static IDictionary<string, string> RetrieveRequestParameters(string parametreString)
        {
            Dictionary<string, string> resultParameters = new Dictionary<string, string>();

            string[] parameters = parametreString.Split('&');

            foreach (string param in parameters)
            {
                string[] pair = param.Split('=');
                string name = pair[0];
                string value = null;

                if (pair.Length > 1)
                {
                    value = pair[1];
                }

                resultParameters.Add(name, value);
            }

            return resultParameters;
        }

        public static ICoockieCollection GetCookies()
        {
            string cookieString = Environment.GetEnvironmentVariable(Constants.CookieGet);

            if (string.IsNullOrEmpty(cookieString))
            {
                return new CoockieCollection();
            }

            var cookies = new CoockieCollection();
            string[] coockieSaves = cookieString.Split(new char[] {';'}, StringSplitOptions.RemoveEmptyEntries);

            foreach (string coockieSave in coockieSaves)
            {
                string[] coockiePair = coockieSave.Split(new char[] {'='}, StringSplitOptions.RemoveEmptyEntries);
                string coockieName = coockiePair[0];
                string coockieValue = null;

                if (coockiePair.Length > 1)
                {
                    coockieValue = coockiePair[1];
                }

                Cookie cookie = new Cookie(coockieName, coockieValue);
                cookies.AddCoockie(cookie);
            }

            return cookies;
        }

        public static Session GetSession()
        {
            ICoockieCollection cookies = GetCookies();

            if (!cookies.ContainsKey(Constants.SessionIdKey))
            {
                return null;
            }

            Cookie sessionCookie = cookies[Constants.SessionIdKey];
            PizzaMoreContext context = new PizzaMoreContext();

            Session session = context.Sessions.FirstOrDefault(x => x.Id == sessionCookie.Value);
            return session;
        }

        public static void PrintFileContent(string path)
        {
            string content = File.ReadAllText(path);
            Console.WriteLine(content);
        }

        public static void PageNotAllowed()
        {
            string path = "../www/pm/game/index.html";
            PrintFileContent(path);
        }
    }
}
