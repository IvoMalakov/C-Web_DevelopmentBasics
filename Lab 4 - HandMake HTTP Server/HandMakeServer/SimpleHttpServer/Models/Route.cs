namespace SimpleHttpServer.Models
{
    using System;
    using Enums;
    public class Route
    {
        public string Name { get; set; }

        public string UrlRegex { get; set; }

        public RequestMethod Method { get; set; }

        public Func<HTTPRequest, HTTPResponse> Callable { get; set; }

    }
}
