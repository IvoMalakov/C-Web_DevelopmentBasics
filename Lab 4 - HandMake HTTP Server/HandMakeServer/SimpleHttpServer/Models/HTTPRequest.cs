namespace SimpleHttpServer.Models
{
    using System;
    using System.Text;
    using Enums;

    public class HTTPRequest
    {

        public HTTPRequest(RequestMethod method, string url) 
        {
            this.Method = method;
            this.Url = url;
            this.Header = new Header(HeaderType.HttpRequest);
        }

        public RequestMethod Method { get; set; }

        public string Url { get; set; }

        public Header Header { get; set; }

        public string Content { get; set; }

        public override string ToString()
        {
            StringBuilder request = new StringBuilder();

            if (this.Method == RequestMethod.GET)
            {
                request.Append("GET ");
            }
            else if (this.Method == RequestMethod.POST)
            {
                request.Append("POST ");
            }
            request.Append($"{this.Url} HTTP/1.0{Environment.NewLine}");
            request.AppendLine(this.Header.ToString());
            if (!string.IsNullOrEmpty(this.Content) && this.Method == RequestMethod.POST)
            {
                request.AppendLine();
                request.AppendLine(this.Content);
            }

            return request.ToString();
        }
    }
}
