namespace PizzaMore.Utility
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces;
    public class CoockieCollection : ICoockieCollection
    {
        private readonly IDictionary<string, Cookie> cookies;

        public CoockieCollection()
        {
            this.cookies = new Dictionary<string, Cookie>();
        }

        public void AddCoockie(Cookie cookie)
        {
            string[] cookieInfo = cookie.ToString().Split('=');
            string name = cookieInfo[0];
            string value = cookieInfo[1];

            Cookie cookieForAdd = new Cookie()
            {
                Name = name,
                Value = value
            };

            this.cookies[name] = cookieForAdd;
        }

        public void RemoveCoockie(string cookieName)
        {
            if (this.cookies.ContainsKey(cookieName))
            {
                this.cookies[cookieName] = null;
                this.cookies.Remove(cookieName);
            }
        }

        public bool ContainsKey(string key)
        {
            string[] keysArray = this.cookies.Keys.ToArray();

            return keysArray.Any(keyForcheck => keyForcheck.Equals(key));
        }

        public int Count
        {
            get { return this.cookies.Keys.ToArray().Length; }
        }

        public Cookie this[string key]
        {
            get { return this.cookies[key]; }
            set { this.cookies[key] = value; }
        }
    }
}
