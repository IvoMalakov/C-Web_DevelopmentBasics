using System.Collections;

namespace SimpleHttpServer.Models
{
    using System.Collections.Generic;

    public class CoockieCollection : IEnumerable<Cookie>
    {
        public CoockieCollection()
        {
            this.Cookies = new Dictionary<string, Cookie>();
        }

        public IDictionary<string, Cookie> Cookies { get; private set; }
        public IEnumerator<Cookie> GetEnumerator()
        {
            return this.Cookies.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public int Count
        {
            get { return this.Cookies.Count; }
        }

        public bool Contains(string coockieName)
        {
            return this.Cookies.ContainsKey(coockieName);
        }

        public void Add(Cookie cookie)
        {
            if (!this.Cookies.ContainsKey(cookie.Name))
            {
                this.Cookies.Add(cookie.Name, new Cookie());
            }

            this.Cookies[cookie.Name] = cookie;
        }

        public Cookie this[string coockieName]
        {
            get
            {
                return this.Cookies[coockieName];
            }

            set
            {
                if (this.Cookies.ContainsKey(coockieName))
                {
                    this.Cookies[coockieName] = value;
                }
                else
                {
                    this.Cookies.Add(coockieName, value);
                }
            }
        }

        public override string ToString()
        {
            return string.Join(", ", Cookies.Values);
        }
    }
}
