namespace SimpleHttpServer
{
    using System;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Sockets;
    using Models;
    using Enums;
    public class HttpProcessor
    {
        private IList<Route> Routes;
        private HTTPRequest Request;
        private HTTPResponse Response;

        public HttpProcessor(IEnumerable<Route> routes)
        {
            this.Routes = new List<Route>(routes);
        }

        public void HandleClient(TcpClient tcpClient)
        {
            var stream = tcpClient.GetStream();

            using (stream)
            {
                this.Request = GetRequest(stream);
                this.Response = RouteRequest();
                StreamUtils.WriteResponse(stream, this.Response);
            }
        }

        private HTTPResponse RouteRequest()
        {
            string requestUrl = this.Request.Url;
            if (!this.Routes.Any(r => new Regex(r.UrlRegex).IsMatch(requestUrl)))
            {
                return ResponseBuilder.NotFound();
            }

            var matchingRoutes = this.Routes.Where(r => new Regex(r.UrlRegex).IsMatch(requestUrl));
            if (matchingRoutes.All(r => r.Method != this.Request.Method))
            {
                return new HTTPResponse(ResponseStatusCode.Method_Not_Allowed);
            }

            Route route = this.Routes
                .FirstOrDefault(r => new Regex(r.UrlRegex).IsMatch(requestUrl) && r.Method == this.Request.Method);

            try
            {
                HTTPResponse response = route.Callable(this.Request);
                return response;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return ResponseBuilder.InternalServerError();
            }
        }

        private HTTPRequest GetRequest(NetworkStream inputStream)
        {
            string firstLine = StreamUtils.ReadLine(inputStream);
            string[] requestLine = firstLine.Split(' ');

            if (requestLine.Length < 3)
            {
                throw new ArgumentException("Missing data");
            }

            RequestMethod method = RequestMethod.GET;
            if (requestLine[0].ToLower() == "post")
            {
                method = RequestMethod.POST;
            }

            string url = requestLine[1];
            Header header = new Header(HeaderType.HttpRequest);
            string line;
            while ((line = StreamUtils.ReadLine(inputStream)) != null)
            {
                if (string.IsNullOrEmpty(line))
                {
                    break;
                }

                string[] headerInfo = line.Split(':');
                string name = headerInfo[0];
                string value = headerInfo[1];

                switch (name)
                {
                    case "Cookie": 
                        Cookie cookie = new Cookie(name, value);
                        header.Coockies.Add(cookie);
                        break;

                    case "Content-Length":
                        header.ContentLength = value;
                        break;

                    default:
                        if (!header.OtherParameters.ContainsKey(name))
                        {
                            header.OtherParameters.Add(name, value);
                        }

                        header.OtherParameters[name] = value;
                        break;
                }
            }

            string content = null;

            if (header.ContentLength != null)
            {
                int totalBytes = Convert.ToInt32(header.ContentLength);
                int bytesLeft = totalBytes;
                byte[] bytes = new byte[totalBytes];

                while (bytesLeft > 0)
                {
                    byte[] buffer = new byte[bytesLeft > 1024 ? 1024 : bytesLeft];
                    int n = inputStream.Read(buffer, 0, buffer.Length);
                    buffer.CopyTo(bytes, totalBytes - bytesLeft);

                    bytesLeft -= n;
                }

                content = Encoding.ASCII.GetString(bytes);
            }

            HTTPRequest request = new HTTPRequest(method, url)
            {
                Content = content,
                Header = header
            };

            return request;
        }
    }
}
