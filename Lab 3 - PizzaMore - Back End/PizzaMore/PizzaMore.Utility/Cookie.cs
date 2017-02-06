namespace PizzaMore.Utility
{
    using System;
    using System.Text;
    public class Cookie
    {
        public Cookie()
        {
            this.Name = null;
            this.Value = null;
        }

        public Cookie(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        public string Name { get; set; }

        public string Value { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}={1}{2}", this.Name, this.Value, Environment.NewLine);

            return sb.ToString();
        }
    }
}
