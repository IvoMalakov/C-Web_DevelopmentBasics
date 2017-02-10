namespace SimpleHttpServer.Models
{
    using System;
    using System.Text;
    using Enums;

    public class HTTPResponse
    {
        private int statusCode;
        private ResponseStatusCode statusMessage;
        private string contentAsUtf8;
        private byte[] content;

        public HTTPResponse(ResponseStatusCode statusMessage)
        {
            this.StatusMessage = statusMessage;
            this.Header = new Header(HeaderType.HttpResponse);
            this.ContentAsUtf8 = contentAsUtf8;
        }

        public int StatusCode
        {
            get
            {
                return this.statusCode;
            }
        }

        public ResponseStatusCode StatusMessage
        {
            get
            {
                return this.statusMessage;
            }

            set
            {
                this.statusCode = Convert.ToInt32(value);
                this.statusMessage = value;
            }
        }

        public Header Header { get; set; }

        public string ContentAsUtf8
        {
            get
            {
                return this.contentAsUtf8;
            }

            set
            {
                this.content = new byte[value.Length];

                if (this.content.Length != 0)
                {
                    this.content = UTF8Encoding.UTF8.GetBytes(value);
                }

                this.contentAsUtf8 = value;
            }
        }

        public byte[] Content
        {
            get
            {
                return this.content;
            }
        }

        public override string ToString()
        {
            StringBuilder response = new StringBuilder();
            string replacedMessage = this.StatusMessage.ToString().Replace('_', ' ');

            response.Append("HTTP/1.0 ");
            response.Append($"{this.StatusCode} {replacedMessage}{Environment.NewLine}");
            response.AppendLine(this.Header.ToString());

            return response.ToString();
        }
    }
}
