namespace TestServer
{
    using System.Collections.Generic;
    using SimpleHttpServer;
    using SimpleHttpServer.Models;
    using SimpleHttpServer.Enums;
    class Test
    {
        static void Main()
        {
            IList<Route> routes = new List<Route>()
            {
                new Route
                {
                    Name = "Hello Handler",
                    UrlRegex = @"^/hello$",
                    Method = RequestMethod.GET,
                    Callable = (HTTPRequest request) =>
                    {
                        return new HTTPResponse(ResponseStatusCode.OK)
                        {
                            ContentAsUtf8 = "<h3>Hello from HttpServer :)</h3>"
                        };
                    }
                }
            };

            HttpServer httpServer = new HttpServer(8081, routes);
            httpServer.Listen();
        }
    }
}
