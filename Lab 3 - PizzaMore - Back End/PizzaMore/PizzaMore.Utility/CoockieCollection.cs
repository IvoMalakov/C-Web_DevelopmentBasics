using System.Collections;

namespace PizzaMore.Utility
{
    using System.Collections.Generic;
    using Interfaces;
    public class CoockieCollection : ICoockieCollection
    {

        public CoockieCollection()
        {
            this.Cookies = new Dictionary<string, Cookie>();
        }

        public IDictionary<string, Cookie> Cookies { get; private set; }

        public void AddCoockie(Cookie cookie)
        {
            if (!this.Cookies.ContainsKey(cookie.Name))
            {
                this.Cookies.Add(cookie.Name, new Cookie());
            }

            this.Cookies[cookie.Name] = cookie;
        }

        public void RemoveCoockie(string cookieName)
        {
            if (this.Cookies.ContainsKey(cookieName))
            {
                this.Cookies.Remove(cookieName);
            }
        }

        public bool ContainsKey(string key)
        {
            return this.Cookies.ContainsKey(key);
        }

        public int Count
        {
            get { return this.Cookies.Count; }
        }

        public Cookie this[string key]
        {
            get
            {
                return this.Cookies[key];
            }

            set
            {
                if (this.Cookies.ContainsKey(key))
                {
                    this.Cookies[key] = value;
                }
                else
                {
                    this.Cookies.Add(key, value);
                }
            }
           
        }

        public IEnumerator<Cookie> GetEnumerator()
        {
            return this.Cookies.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
