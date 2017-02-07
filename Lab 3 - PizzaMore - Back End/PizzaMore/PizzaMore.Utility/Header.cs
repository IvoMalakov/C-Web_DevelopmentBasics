namespace PizzaMore.Utility
{
    using System;
    using System.Text;
    using Interfaces;

    public class Header
    {
        public Header()
        {
            this.Type = "Content-type: text/html";
            this.Cookies = new CoockieCollection();
        }

        public string Type { get; set; }

        public string Location { get; private set; }

        public ICoockieCollection Cookies { get; private set; }

        public void AddLocation(string location)
        {
            this.Location = $"Location: {location}";
        }

        public void AddCoockie(Cookie cookie)
        {
            this.Cookies.AddCoockie(cookie);
        }

        public void Print()
        {
            Console.WriteLine(this.ToString());
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(this.Type);

            if (this.Cookies.Count > 0)
            {
                foreach (Cookie cookie in this.Cookies)
                {
                    sb.AppendLine($"Set-Cookie: {cookie.ToString()}");
                }
            }

            if (this.Location != null)
            {
                sb.AppendLine(this.Location);
            }

            sb.AppendLine();
            sb.AppendLine();

            return sb.ToString();
        }
    }
}
