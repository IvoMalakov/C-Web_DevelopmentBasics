using System.Net;

namespace PizzaMore
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using SimpleHttpServer;
    using SimpleHttpServer.Models;
    using SimpleHttpServer.Enums;
    using PizzaMore.Data;
    using PizzaMore.Data.Models;

    class PizzaMoreStart
    {
        private static void Main(string[] args)
        {
            PizzaMoreContext context = new PizzaMoreContext();

            IList<Route> routes = new List<Route>()
            {
                new Route()
                {
                    Name = "Home directory",
                    Method = SimpleHttpServer.Enums.RequestMethod.GET,
                    UrlRegex = "^/home$",
                    Callable = (request) =>
                    {
                        string language = GetLanguageCoockie(request);
                        string fileName = "home.html";
                        if (language == "DE")
                        {
                            fileName = "home-de.html";
                        }

                        var response = new HttpResponse()
                        {
                            StatusCode = SimpleHttpServer.Enums.ResponseStatusCode.Ok,
                            ContentAsUTF8 = File.ReadAllText($"../../content/{fileName}")
                        };

                        return response;
                    }
                },

                new Route()
                {
                    Name = "Home directory",
                    Method = SimpleHttpServer.Enums.RequestMethod.POST,
                    UrlRegex = "^/home$",
                    Callable = (request) =>
                    {
                        string lang = GetLangParam(request);
                        string filename = "home.html";
                        if (lang == "DE")
                        {
                            filename = "home-de.html";
                        }

                        var response = new HttpResponse()
                        {
                            StatusCode = SimpleHttpServer.Enums.ResponseStatusCode.Ok,
                            ContentAsUTF8 = File.ReadAllText($"../../content/{filename}")
                        };

                        Cookie langCookie = new Cookie("lang", lang);
                        response.Header.AddCookie(langCookie);
                        return response;
                    }
                },

                new Route()
                {
                    Name = "Signup Directory",
                    Method = SimpleHttpServer.Enums.RequestMethod.GET,
                    UrlRegex = "^/signup$",
                    Callable = (request) =>
                    {
                        return new HttpResponse()
                        {
                            StatusCode = SimpleHttpServer.Enums.ResponseStatusCode.Ok,
                            ContentAsUTF8 = File.ReadAllText("../../content/signup.html")
                        };
                    }
                },

                new Route()
                {
                    Name = "Signup Directory",
                    Method = SimpleHttpServer.Enums.RequestMethod.POST,
                    UrlRegex = "^/signup$",
                    Callable = (request) =>
                    {
                        RegisterUser(request, context);

                        var response = new HttpResponse()
                        {
                            StatusCode = SimpleHttpServer.Enums.ResponseStatusCode.Ok,
                            ContentAsUTF8 = File.ReadAllText("../../content/signup.html")
                        };

                        return response;
                    }
                },

                new Route()
                {
                    Name = "Signin Directory",
                    Method = SimpleHttpServer.Enums.RequestMethod.GET,
                    UrlRegex = "^/signin$",
                    Callable = (request) =>
                    {
                        return new HttpResponse()
                        {
                            StatusCode = SimpleHttpServer.Enums.ResponseStatusCode.Ok,
                            ContentAsUTF8 = File.ReadAllText("../../content/signin.html")
                        };
                    }
                },

                new Route()
                {
                    Name = "Signin Directory",
                    Method = SimpleHttpServer.Enums.RequestMethod.POST,
                    UrlRegex = "^/signin$",
                    Callable = (request) =>
                    {
                        Cookie sessionCoockie = LoginUser(request, context);

                        var response = new HttpResponse()
                        {
                            StatusCode = SimpleHttpServer.Enums.ResponseStatusCode.Ok,
                            ContentAsUTF8 = File.ReadAllText("../../content/signin.html")
                        };

                        response.Header.Cookies.Add(sessionCoockie);
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
                },

                new Route()
                {
                    Name = "Boostrap JS",
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
                    Name = "Load Custom CSS",
                    Method = SimpleHttpServer.Enums.RequestMethod.GET,
                    UrlRegex = @"^/css/.+\.css$",
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
                    UrlRegex = @"^/jquery/jquery-3.1.1.js$",
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
                    UrlRegex = @"^/images/.+",
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
                }
            };

            HttpServer server = new HttpServer(9091, routes);
            server.Listen();
        }

        private static Cookie LoginUser(HttpRequest request, PizzaMoreContext context)
        {
            User checkedUser = ExtractUserInfoFromAForm(request);

            User userFromDb = context.Users.SingleOrDefault(u => u.Email == checkedUser.Email);

            if (userFromDb.Password == checkedUser.Password)
            {
                Random rnd = new Random();

                Session session = new Session()
                {
                    User = userFromDb,
                    Id = rnd.Next(0, Int32.MaxValue).ToString()
                };

                context.Sessions.Add(session);
                context.SaveChanges();

                Cookie sessionCookie = new Cookie("sid", session.Id);
                return sessionCookie;
            }

            return null;
        }

        private static void RegisterUser(HttpRequest request, PizzaMoreContext context)
        {
            User user = ExtractUserInfoFromAForm(request);

            context.Users.Add(user);
            context.SaveChanges();
        }

        private static User ExtractUserInfoFromAForm(HttpRequest request)
        {
            IDictionary<string, string> nameValuePair = new Dictionary<string, string>();

            string decodeString = WebUtility.UrlDecode(request.Content);
            string[] userInfo = decodeString.Split('&');

            foreach (string userParam in userInfo)
            {
                string[] parametreInfo = userParam.Split('=');

                if (parametreInfo.Length != 2)
                {
                    throw new ArgumentException("Error! Missing Parametre");
                }

                string paramName = parametreInfo[0];
                string paramValue = parametreInfo[1];

                nameValuePair.Add(paramName, paramValue);
            }

            string userEmail = nameValuePair["email"];
            string password = nameValuePair["password"];

            User user = new User()
            {
                Email = userEmail,
                Password = PasswordHasher.Hash(password)
            };
            return user;
        }

        private static string GetLangParam(HttpRequest request)
        {
            string decodeString = WebUtility.UrlDecode(request.Content);
            string[] langInfo = decodeString.Split('=');
            return langInfo[1];
        }

        private static string GetLanguageCoockie(HttpRequest request)
        {
            if (!request.Header.Cookies.Contains("lang"))
            {
                AddDefaultLanguageCoockie(request);
            }

            string answer = request.Header.Cookies["lang"].Value;
            return answer;
        }

        private static void AddDefaultLanguageCoockie(HttpRequest request)
        {
            Cookie languageCookie = new Cookie("lang", "EN");
            request.Header.Cookies.Add(languageCookie);
        }
    }
}
