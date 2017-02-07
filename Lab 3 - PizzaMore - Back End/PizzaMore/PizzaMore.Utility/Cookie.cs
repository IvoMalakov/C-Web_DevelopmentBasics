namespace PizzaMore.Utility
{
    using System.Text;
    public class Cookie
    {
        public Cookie() : this(null, null)
        {

        }

        public Cookie(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        public string Name { get; private set; }

        public string Value { get; private set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}={1}", this.Name, this.Value);

            return sb.ToString();
        }
    }
}
