namespace PizzaMore.Services
{
    using System.Net;
    using SimpleHttpServer.Models;
    using Coockie = SimpleHttpServer.Models.Cookie;
    public class LanguageService
    {
        public static string GetLangParam(HttpRequest request)
        {
            string decodeString = WebUtility.UrlDecode(request.Content);
            string[] langInfo = decodeString.Split('=');
            return langInfo[1];
        }

        public static string GetLanguageCoockie(HttpRequest request)
        {
            if (!request.Header.Cookies.Contains("lang"))
            {
                AddDefaultLanguageCoockie(request);
            }

            string answer = request.Header.Cookies["lang"].Value;
            return answer;
        }

        private static void AddDefaultLanguageCoockie(HttpRequest request)
        {
            Coockie languageCookie = new Coockie("lang", "EN");
            request.Header.Cookies.Add(languageCookie);
        }
    }
}
