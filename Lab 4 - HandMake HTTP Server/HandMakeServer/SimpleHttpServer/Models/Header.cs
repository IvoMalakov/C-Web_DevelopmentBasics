namespace SimpleHttpServer.Models
{
    using System.Text;
    using System.Collections.Generic;
    using Enums;

    public class Header
    {
        public Header(HeaderType type)
        {
            this.Type = type;
            this.ContentType = "text/html";
            this.Coockies = new CoockieCollection();
            this.OtherParameters = new Dictionary<string, string>();
        }

        public HeaderType Type { get; set; }

        public string ContentType { get; set; }

        public string ContentLength { get; set; }

        public Dictionary<string, string> OtherParameters { get; set; }

        public CoockieCollection Coockies { get; private set; }

        public override string ToString()
        {
            StringBuilder header = new StringBuilder();

            header.AppendLine("Content-type: " + this.ContentType);
            if (this.Coockies.Count > 0)
            {
                if (this.Type == HeaderType.HttpRequest)
                {
                    header.AppendLine("Coockie: " + this.Coockies.ToString());
                }
                else if (this.Type == HeaderType.HttpResponse)
                {
                    foreach (Cookie coockie in this.Coockies)
                    {
                        header.AppendLine("Set-Coockie: " + coockie);
                    }
                }
            }
            if (this.ContentLength != null)
            {
                header.AppendLine("Content-Length: " + this.ContentLength);
            }
            foreach (var other in OtherParameters)
            {
                header.AppendLine($"{other.Key}: {other.Value}");
            }
            header.AppendLine();

            return header.ToString();
        }
    }
}
