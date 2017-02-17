using System.Net;

namespace PizzaMore
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
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
                    Name = "Log Out",
                    Method = SimpleHttpServer.Enums.RequestMethod.GET,
                    UrlRegex = @"^/home\?logout=true$",
                    Callable = (request) =>
                    {
                        LogoutUser(request, context);

                        var response = new HttpResponse()
                        {
                            StatusCode = SimpleHttpServer.Enums.ResponseStatusCode.Ok,
                            ContentAsUTF8 = File.ReadAllText("../../content/home.html")
                        };

                        response.Header.Cookies.Remove("sid");
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
                    Name = "Menu Directory",
                    Method = SimpleHttpServer.Enums.RequestMethod.GET,
                    UrlRegex = "^/menu$",
                    Callable = (request) =>
                    {
                        bool haveSession = CheckSession(request, context);

                        if (!haveSession)
                        {
                            return new HttpResponse()
                            {
                                StatusCode = SimpleHttpServer.Enums.ResponseStatusCode.Forbidden,
                                ContentAsUTF8 = File.ReadAllText("../../content/403.html")
                            };
                        }
                        else
                        {
                            string userEmail = GetUserEmail(request, context);
                            GenerateAllSuggestions(context);

                            StringBuilder sb = new StringBuilder();
                            sb.AppendLine($"<p class=\"navbar-text navbar-right\">Signed in as: {userEmail}</p>");
                            
                            return new HttpResponse()
                            {
                                StatusCode = SimpleHttpServer.Enums.ResponseStatusCode.Ok,
                                ContentAsUTF8 = File.ReadAllText("../../content/menu-top.html") +
                                                sb.ToString() +
                                                File.ReadAllText("../../content/menu-middle.html") + 
                                                File.ReadAllText("../../content/all-suggestions.html")
                            };
                        }
                    }
                },

                new Route()
                {
                    Name = "Add Pizza Directory",
                    Method = SimpleHttpServer.Enums.RequestMethod.GET,
                    UrlRegex = "^/addpizza$",
                    Callable = (request) =>
                    {
                        bool haveSession = CheckSession(request, context);

                        if (!haveSession)
                        {
                            return new HttpResponse()
                            {
                                StatusCode = SimpleHttpServer.Enums.ResponseStatusCode.Forbidden,
                                ContentAsUTF8 = File.ReadAllText("../../content/403.html")
                            };
                        }
                        else
                        { 
                            return new HttpResponse()
                            {
                                StatusCode = SimpleHttpServer.Enums.ResponseStatusCode.Ok,
                                ContentAsUTF8 = File.ReadAllText("../../content/addpizza.html")
                            };
                        }
                    }
                },

                new Route()
                {
                    Name = "Add Pizza Directory",
                    Method = SimpleHttpServer.Enums.RequestMethod.POST,
                    UrlRegex = "^/addpizza$",
                    Callable = (request) =>
                    {
                        bool haveSession = CheckSession(request, context);

                        if (!haveSession)
                        {
                            return new HttpResponse()
                            {
                                StatusCode = SimpleHttpServer.Enums.ResponseStatusCode.Forbidden,
                                ContentAsUTF8 = File.ReadAllText("../../content/403.html")
                            };
                        }
                        else
                        {
                            AddPizza(request, context);

                            return new HttpResponse()
                            {
                                StatusCode = SimpleHttpServer.Enums.ResponseStatusCode.Ok,
                                ContentAsUTF8 = File.ReadAllText("../../content/addpizza.html")
                            };
                        }
                    }
                },

                 new Route()
                {
                    Name = "Yoursuggestions Directory",
                    Method = SimpleHttpServer.Enums.RequestMethod.GET,
                    UrlRegex = "^/yoursuggestions$",
                    Callable = (request) =>
                    {
                        bool haveSession = CheckSession(request, context);

                        if (!haveSession)
                        {
                            return new HttpResponse()
                            {
                                StatusCode = SimpleHttpServer.Enums.ResponseStatusCode.Forbidden,
                                ContentAsUTF8 = File.ReadAllText("../../content/403.html")
                            };
                        }
                        else
                        {
                            string suggestions = GetListOfSuggestedItems(request, context);

                            return new HttpResponse()
                            {
                                StatusCode = SimpleHttpServer.Enums.ResponseStatusCode.Ok,
                                ContentAsUTF8 = File.ReadAllText("../../content/yoursuggestions-top.html") +
                                                suggestions +
                                                File.ReadAllText("../../content/yoursuggestions-bottom.html")
                            };
                        }
                    }
                },

                new Route()
                {
                    Name = "DetailsPizza Directory",
                    Method = SimpleHttpServer.Enums.RequestMethod.GET,
                    UrlRegex = @"^/DetailsPizza\?pizzaid=.+$",
                    Callable = (request) =>
                    {
                        bool haveSession = CheckSession(request, context);

                        if (!haveSession)
                        {
                            return new HttpResponse()
                            {
                                StatusCode = SimpleHttpServer.Enums.ResponseStatusCode.Forbidden,
                                ContentAsUTF8 = File.ReadAllText("../../content/403.html")
                            };
                        }
                        else
                        {
                            int lastIndexOfEquals = request.Url.LastIndexOf('=');
                            int pizzaId = int.Parse(request.Url.Substring(lastIndexOfEquals + 1));
                            string content = GeneratePizzaDetails(request, context, pizzaId);

                            return new HttpResponse()
                            {
                                StatusCode = SimpleHttpServer.Enums.ResponseStatusCode.Ok,
                                ContentAsUTF8 = content
                            };
                        }
                    }
                },

                new Route()
                {
                    Name = "Menu Directory",
                    Method = SimpleHttpServer.Enums.RequestMethod.POST,
                    UrlRegex = "^/menu$",
                    Callable = (request) =>
                    {
                        bool haveSession = CheckSession(request, context);

                        if (!haveSession)
                        {
                            return new HttpResponse()
                            {
                                StatusCode = SimpleHttpServer.Enums.ResponseStatusCode.Forbidden,
                                ContentAsUTF8 = File.ReadAllText("../../content/403.html")
                            };
                        }
                        else
                        {
                            VoteForPizza(request, context);
                            GenerateAllSuggestions(context);
                            string userEmail = GetUserEmail(request, context);

                            StringBuilder sb = new StringBuilder();
                            sb.AppendLine($"<p class=\"navbar-text navbar-right\">Signed in as: {userEmail}</p>");

                            return new HttpResponse()
                            {
                                StatusCode = SimpleHttpServer.Enums.ResponseStatusCode.Ok,
                                ContentAsUTF8 = File.ReadAllText("../../content/menu-top.html") +
                                                sb.ToString() +
                                                File.ReadAllText("../../content/menu-middle.html") +
                                                File.ReadAllText("../../content/all-suggestions.html")
                            };
                        }
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

        private static void LogoutUser(HttpRequest request, PizzaMoreContext context)
        {
            Session session = context.Sessions.Find(request.Header.Cookies["sid"].Value);
            context.Sessions.Remove(session);
            context.SaveChanges();
        }

        private static string GeneratePizzaDetails(HttpRequest request, PizzaMoreContext context, int pizzaId)
        {
            Pizza pizza = context.Pizzas.Find(pizzaId);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<!doctype html><html lang=\"en\"><head><meta charset=\"UTF-8\" /><title>PizzaMore - Details</title><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\" /><link rel=\"stylesheet\" href=\"/bootstrap/css/bootstrap.min.css\" /><link rel=\"stylesheet\" href=\"/css/signin.css\" /></head><body><div class=\"container\">");
            sb.AppendLine("<div class=\"jumbotron\">");
            sb.AppendLine("<a class=\"btn btn-danger\" href=\"/menu\">All Suggestions</a>");
            sb.AppendLine($"<h3>{pizza.Title}</h3>");
            sb.AppendLine($"<img src=\"{pizza.ImageUrl}\" width=\"300px\"/>");
            sb.AppendLine($"<p>{pizza.Recipe}</p>");
            sb.AppendLine($"<p>Up: {pizza.UpVotes}</p>");
            sb.AppendLine($"<p>Down: {pizza.DownVotes}</p>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div><script src=\"/jquery/jquery-3.1.1.js\"></script><script src=\"/bootstrap/js/bootstrap.min.js\"></script></body></html>");

            return sb.ToString();
        }

        private static string GetListOfSuggestedItems(HttpRequest request, PizzaMoreContext context)
        {
            Session session = context.Sessions.Find(request.Header.Cookies["sid"].Value);
            User user = context.Users.Find(session.UserId);
            var suggestions = context.Pizzas.Where(p => p.OwnerId == user.Id);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<ul>");

            foreach (var suggestion in suggestions)
            {
                sb.AppendLine("<form method=\"POST\">");
                sb.AppendLine($"<li><a href=\"/DetailsPizza?pizzaid={suggestion.Id}\">{suggestion.Title}</a> <input type=\"hidden\" name=\"pizzaId\" value=\"{suggestion.Id}\"/> <input type=\"submit\" class=\"btn btn-sm btn-danger\" value=\"X\"/></li>");
                sb.AppendLine("</form>");
            }

            sb.AppendLine("</ul>");
            return sb.ToString();
        }

        private static void AddPizza(HttpRequest request, PizzaMoreContext context)
        {
            IDictionary<string, string> addPizzaDictionary = new Dictionary<string, string>();

            string decodeString = WebUtility.UrlDecode(request.Content);
            string[] pizzaParams = decodeString.Split('&');

            foreach (string pizzaParam in pizzaParams)
            {
                string[] parametres = pizzaParam.Split('=');
                string paramName = parametres[0];
                string paramValue = parametres[1];

                addPizzaDictionary.Add(paramName, paramValue);
            }

            Session session = context.Sessions.Find(request.Header.Cookies["sid"].Value);
            User user = context.Users.Find(session.UserId);

            Pizza pizza = new Pizza()
            {
                Title = addPizzaDictionary["title"],
                Recipe = addPizzaDictionary["recipe"],
                ImageUrl = addPizzaDictionary["url"],
                OwnerId = user.Id,
                Owner = user
            };

            context.Pizzas.Add(pizza);
            context.SaveChanges();

        }

        private static string GetUserEmail(HttpRequest request, PizzaMoreContext context)
        {
            Session session = context.Sessions.Find(request.Header.Cookies["sid"].Value);
            User user = context.Users.Find(session.UserId);

            string userEmail = user.Email;
            return userEmail;
        }

        private static void VoteForPizza(HttpRequest request, PizzaMoreContext context)
        {
            IDictionary<string, string> pizzaParamsDictionary = new Dictionary<string, string>();

            string decodeString = WebUtility.UrlDecode(request.Content);
            string[] pizzaVoteDetails = decodeString.Split('&');

            foreach (string pizaParams in pizzaVoteDetails)
            {
                string[] pizaInfo = pizaParams.Split('=');
                string paramName = pizaInfo[0];
                string paramValue = pizaInfo[1];

                pizzaParamsDictionary.Add(paramName, paramValue);
            }

            Pizza pizza = context.Pizzas.Find(int.Parse(pizzaParamsDictionary["pizzaid"]));
            string vote = pizzaParamsDictionary["pizzaVote"];

            switch (vote)
            {
                case "up":
                    pizza.UpVotes++;
                    break;

                case "down":
                    pizza.DownVotes++;
                    break;
            }

            context.SaveChanges();
        }

        private static bool CheckSession(HttpRequest request, PizzaMoreContext context)
        {
            Session session = context.Sessions.Find(request.Header.Cookies["sid"].Value);

            if (session == null)
            {
                return false;
            }

            return true;
        }

        private static void GenerateAllSuggestions(PizzaMoreContext context)
        {
            StringBuilder sb = new StringBuilder();

            var pizzas = context.Pizzas;
            sb.AppendLine("<div class=\"card-deck\">");
            foreach (var pizza in pizzas)
            {
                sb.AppendLine("<div class=\"card\">");
                sb.AppendLine($"<img class=\"card-img-top\" src=\"{pizza.ImageUrl}\" width=\"200px\"alt=\"Card image cap\">");
                sb.AppendLine("<div class=\"card-block\">"); Console.WriteLine($"<h4 class=\"card-title\">{pizza.Title}</h4>");
                sb.AppendLine($"<p class=\"card-text\"><a href=\"/DetailsPizza?pizzaid={pizza.Id}\">Recipe</a></p>");
                sb.AppendLine("<form method=\"POST\">");
                sb.AppendLine($"<div class=\"radio\"><label><input type = \"radio\" name=\"pizzaVote\" value=\"up\">Up</label></div>");
                sb.AppendLine($"<div class=\"radio\"><label><input type = \"radio\" name=\"pizzaVote\" value=\"down\">Down</label></div>");
                sb.AppendLine($"<input type=\"hidden\" name=\"pizzaid\" value=\"{pizza.Id}\" />");
                sb.AppendLine("<input type=\"submit\" class=\"btn btn-primary\" value=\"Vote\" />");
                sb.AppendLine("</form>");
                sb.AppendLine("</div>");
                sb.AppendLine("</div>");
            }
            sb.AppendLine("</div></div></div></body></html>");

            File.WriteAllText("../../content/all-suggestions.html", sb.ToString());
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
