namespace SoftUniStoreApp
{
    using System.IO;
    using System.Collections.Generic;
    using SimpleHttpServer.Enums;
    using SimpleHttpServer.Models;
    using SimpleMVC.Routers;

    public static class RouterTable
    {
        public static IEnumerable<Route> Routes
        {
            get
            {
                return new Route[]
                {
                    new Route()
                {
                    Name = "Bootstrap CSS",
                    Method = SimpleHttpServer.Enums.RequestMethod.GET,
                    UrlRegex = "/bootstrap/css/bootstrap.min.css$",
                    Callable = (request) =>
                    {
                        var response = new HttpResponse()
                        {
                            StatusCode = SimpleHttpServer.Enums.ResponseStatusCode.Ok,
                            ContentAsUTF8 = File.ReadAllText("../../content/bootstrap/css/bootstrap.min.css")
                        };

                        response.Header.ContentType = "text/css";
                        return response;
                    }
                },

                new Route()
                {
                    Name = "Boostrap JS",
                    Method = SimpleHttpServer.Enums.RequestMethod.GET,
                    UrlRegex = "/bootstrap/js/bootstrap.min.js$",
                    Callable = (request) =>
                    {
                        var response = new HttpResponse()
                        {
                            StatusCode = SimpleHttpServer.Enums.ResponseStatusCode.Ok,
                            ContentAsUTF8 = File.ReadAllText("../../content/bootstrap/js/bootstrap.min.js")
                        };

                        response.Header.ContentType = "application/x-javascript";
                        return response;
                    }
                },

                new Route()
                {
                    Name = "Load Custom CSS",
                    Method = SimpleHttpServer.Enums.RequestMethod.GET,
                    UrlRegex = @"/css/.+\.css$",
                    Callable = (request) =>
                    {
                        int lastIndexOfDash = request.Url.LastIndexOf('/');
                        string fileName = request.Url.Substring(lastIndexOfDash + 1);

                        var response = new HttpResponse()
                        {
                            StatusCode = SimpleHttpServer.Enums.ResponseStatusCode.Ok,
                            ContentAsUTF8 = File.ReadAllText($"../../content/css/{fileName}")
                        };

                        response.Header.ContentType = "text/css";
                        return response;
                    }
                },

                new Route()
                {
                    Name = "Load JQuery",
                    Method = SimpleHttpServer.Enums.RequestMethod.GET,
                    UrlRegex = @"/jquery/jquery-3.1.1.js$",
                    Callable = (request) =>
                    {
                        var response = new HttpResponse()
                        {
                            StatusCode = SimpleHttpServer.Enums.ResponseStatusCode.Ok,
                            ContentAsUTF8 = File.ReadAllText("../../content/jquery/jquery-3.1.1.js")
                        };

                        response.Header.ContentType = "application/x-javascript";
                        return response;
                    }
                },

                new Route()
                {
                    Name = "Load Images",
                    Method = SimpleHttpServer.Enums.RequestMethod.GET,
                    UrlRegex = @"/images/.+",
                    Callable = (request) =>
                    {
                        int lastIndexOfDash = request.Url.LastIndexOf('/');
                        string fileName = request.Url.Substring(lastIndexOfDash + 1);

                        var response = new HttpResponse()
                        {
                            StatusCode = SimpleHttpServer.Enums.ResponseStatusCode.Ok,
                            Content = File.ReadAllBytes($"../../content/images/{fileName}")
                        };

                        response.Header.ContentType = "images/*";
                        response.Header.ContentLength = response.Content.Length.ToString();
                        return response;
                    }
                },

                new Route()
                {
                    Name = "Controller/Action/GET",
                    Method = RequestMethod.GET,
                    UrlRegex = @"^/(.+)/(.+)",
                    Callable = new ControllerRouter().Handle
                },

                new Route()
                {
                    Name = "Controller/Action/POST",
                    Method = RequestMethod.POST,
                    UrlRegex = @"^/(.+)/(.+)",
                    Callable = new ControllerRouter().Handle
                },

                };
            }
        }
    }
}
