namespace SimpleHttpServer.Models
{
    using System;
    using System.Text;
    using Enums;

    public class HTTPResponse
    {
        public HTTPResponse(ResponseStatusCode statusCode)
        {
            this.StatusCode = statusCode;
            this.Header = new Header(HeaderType.HttpResponse);
            this.Content = new byte[10];
        }

        public ResponseStatusCode StatusCode { get; set; }

        public Header Header { get; set; }

        public byte[] Content { get; set; }

        public string ContentAsUtf8
        {
            set
            {
                this.Content = Encoding.UTF8.GetBytes(value);
            }
        }


        public override string ToString()
        {
            StringBuilder response = new StringBuilder();
            string statusCodeWIthSpaces = this.StatusCode.ToString().Replace(" ", " ");

            response.AppendLine($"HTTP/1.0 {(int) this.StatusCode} {statusCodeWIthSpaces}");
            response.AppendLine(this.Header.ToString());

            return response.ToString();
        }
    }
}
