using SimpleHttpServer.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using SimpleHttpServer;
using SharpStore.Data;
using SharpStore.Models;
using SimpleHttpServer.Enums;

namespace SharpStore
{
    class SharpStore
    {
        static void Main(string[] args)
        {
            SharpStoreContext context = new SharpStoreContext();

            var routes = new List<Route>()
            {
                new Route()
                {
                    Name = "Home Directory",
                    Method = SimpleHttpServer.Enums.RequestMethod.GET,
                    UrlRegex = "^/home$",
                    Callable = (request) =>
                    {
                        return new HttpResponse()
                        {
                            StatusCode = SimpleHttpServer.Enums.ResponseStatusCode.Ok,
                            ContentAsUTF8 = File.ReadAllText("../../content/home.html")
                        };
                    }
                },
                new Route()
                {
                    Name = "About Directory",
                    Method = SimpleHttpServer.Enums.RequestMethod.GET,
                    UrlRegex = "^/about$",
                    Callable = (request) =>
                    {
                        return new HttpResponse()
                        {
                            StatusCode = SimpleHttpServer.Enums.ResponseStatusCode.Ok,
                            ContentAsUTF8 = File.ReadAllText("../../content/about.html")
                        };
                    }
                },
                 new Route()
                {
                    Name = "Product Directory",
                    Method = SimpleHttpServer.Enums.RequestMethod.GET,
                    UrlRegex = "^/products$",
                    Callable = (request) =>
                    {
                        var knives = context.Knives.ToList();
                        string porductsFinal = GenerateKnives(knives);
                        return new HttpResponse()
                        {
                            StatusCode = SimpleHttpServer.Enums.ResponseStatusCode.Ok,
                            ContentAsUTF8 = porductsFinal
                        };
                    }
                },
                new Route()
                {
                    Name = "Product Directory",
                    Method = SimpleHttpServer.Enums.RequestMethod.POST,
                    UrlRegex = "^/products$",
                    Callable = (request) =>
                    {
                        string searchFilter = request.Content.Split('=')[1];
                        var knives = context.Knives.Where(k => k.Name.Contains(searchFilter)).ToList();
                        string porductsFinal = GenerateKnives(knives);
                        return new HttpResponse()
                        {
                            StatusCode = SimpleHttpServer.Enums.ResponseStatusCode.Ok,
                            ContentAsUTF8 = porductsFinal
                        };
                    }
                },
                new Route()
                {
                    Name = "Contact Directory",
                    Method = SimpleHttpServer.Enums.RequestMethod.GET,
                    UrlRegex = "^/contacts$",
                    Callable = (request) =>
                    {
                        return new HttpResponse()
                        {
                            StatusCode = SimpleHttpServer.Enums.ResponseStatusCode.Ok,
                            ContentAsUTF8 = File.ReadAllText("../../content/contacts.html")
                        };
                    }
                },
                new Route()
                {
                    Name = "Contact Directory",
                    Method = SimpleHttpServer.Enums.RequestMethod.POST,
                    UrlRegex = "^/contacts$",
                    Callable = (request) =>
                    {
                        UpploadMessageToDB(request, context);

                        return new HttpResponse()
                        {
                            StatusCode = SimpleHttpServer.Enums.ResponseStatusCode.Ok,
                            ContentAsUTF8 = File.ReadAllText("../../content/contacts.html")
                        };
                    }
                },
                new Route()
                {
                    Name = "Images",
                    Method = SimpleHttpServer.Enums.RequestMethod.GET,
                    UrlRegex = @"^/images/.+",
                    Callable = (request) =>
                    {
                        var nameOfFile = request.Url.Substring(request.Url.LastIndexOf('/') + 1);
                        var response = new HttpResponse()
                        {
                            StatusCode = SimpleHttpServer.Enums.ResponseStatusCode.Ok,
                            Content = File.ReadAllBytes($"../../content/images/{nameOfFile}")
                        };
                        response.Header.ContentType = "images/*";
                        response.Header.ContentLength = response.Content.Length.ToString();

                        return response;
                    }
                },
                new Route()
                {
                    Name = "Carousel CSS",
                    Method = SimpleHttpServer.Enums.RequestMethod.GET,
                    UrlRegex = "^/content/css/carousel.css$",
                    Callable = (request) =>
                    {
                        var response = new HttpResponse()
                        {
                            StatusCode = SimpleHttpServer.Enums.ResponseStatusCode.Ok,
                            ContentAsUTF8 = File.ReadAllText("../../content/css/carousel.css")
                        };
                        response.Header.ContentType = "text/css";
                        return response;
                    }
                },
                new Route()
                {
                    Name = "Bootstrap JS",
                    Method = SimpleHttpServer.Enums.RequestMethod.GET,
                    UrlRegex = "^/bootstrap/js/bootstrap.min.js$",
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
                    Name = "Bootstrap CSS",
                    Method = SimpleHttpServer.Enums.RequestMethod.GET,
                    UrlRegex = "^/bootstrap/css/bootstrap.min.css$",
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
                }
            };

            SimpleHttpServer.HttpServer httpServer = new HttpServer(8081, routes);
            httpServer.Listen();
        }

        private static void UpploadMessageToDB(HttpRequest request, SharpStoreContext context)
        {
            string requestContent = WebUtility.UrlDecode(request.Content);
            string[] parameters = requestContent.Split('&');
            IDictionary<string, string> nameValuePairs = new Dictionary<string, string>();

            foreach (var parameter in parameters)
            {
                string[] parameterInfo = parameter.Split('=');
                nameValuePairs.Add(parameterInfo[0], parameterInfo[1]);
            }

            Message message = new Message()
            {
                Sender = nameValuePairs["email"],
                Subject = nameValuePairs["subject"],
                MessageText = nameValuePairs["message"]
            };

            context.Messages.Add(message);
            context.SaveChanges();
        }

        private static string GenerateKnives(List<Knife> knives)
        {
            int counter = 0;
            string productsMiddle = "";

            foreach (var knife in knives)
            {
                if (counter % 3 == 0)
                {
                    productsMiddle += "<section class=\"container\">" + 
                        "<div class=\"row\" margin-top=\"10px\">";
                }

                productsMiddle +=
                    "<div class=\"img-thumbnail\" style=\"margin:10px; padding=10px\">\r\n" +
                    $"<img src=\"{knife.ImageUrl}\" alt=\"Card image cap\" width=\"300\" height=\"150\">\r\n" +
                    "<div class=\"card-block\">\r\n " +
                    $"<h3 class=\"card-title\">{knife.Name}</h3>\r\n" +
                    $"<p class=\"card-text\">{knife.Price}$</p>\r\n" +
                    "<button class=\"btn btn-primary\" style=\"margin-bottom: 10px\" type=\"submit\">Buy now</button>\r\n" +
                    "</div>\r\n" +
                    "</div>";

                if (counter % 3 == 2)
                {
                    productsMiddle += "</div>" +
                                      "</section>";
                }

                counter++;
            }

            string productsFinal = File.ReadAllText("../../content/products-top.html");
            productsFinal += productsMiddle;
            productsFinal += File.ReadAllText("../../content/products-bottom.html");

            return productsFinal;
        }
    }
}