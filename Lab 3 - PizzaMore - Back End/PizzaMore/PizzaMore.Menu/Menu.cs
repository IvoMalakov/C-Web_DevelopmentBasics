﻿namespace PizzaMore.Menu
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models;
    using Data;
    using Utility;

    public class Menu
    {
        private static Session Session;
        private static IDictionary<string, string> PostParameters;
        private static Header Header = new Header();

        public static void Main()
        {
            Session = WebUtil.GetSession();

            if (Session != null)
            {
                if (WebUtil.IsGet())
                {
                    ShowPage();
                }

                else if (WebUtil.IsPost())
                {
                    VoteForPizza();
                    ShowPage();
                }
            }

            else
            {
                Header.Print();
                WebUtil.PageNotAllowed();
            }
        }

        private static void ShowPage()
        {
            Header.Print();
            GenerateNavbar();
            WebUtil.PrintFileContent("../www/pm/menu-top.html");
            GenerateAllSuggestions();
            WebUtil.PrintFileContent("../www/pm/menu-bottom.html");
        }

        private static void GenerateNavbar()
        {
            Console.WriteLine("<nav class=\"navbar navbar-default\">" +
                              "<div class=\"container-fluid\">" +
                              "<div class=\"navbar-header\">" +
                              "<a class=\"navbar-brand\" href=\"Home.exe\">PizzaMore</a>" +
                              "</div>" +
                              "<div class=\"collapse navbar-collapse\" id=\"bs-example-navbar-collapse-1\">" +
                              "<ul class=\"nav navbar-nav\">" +
                              "<li ><a href=\"AddPizza.exe\">Suggest Pizza</a></li>" +
                              "<li><a href=\"YourSuggestions.exe\">Your Suggestions</a></li>" +
                              "</ul>" +
                              "<ul class=\"nav navbar-nav navbar-right\">" +
                              "<p class=\"navbar-text navbar-right\"></p>" +
                              "<p class=\"navbar-text navbar-right\"><a href=\"Home.exe?logout=true\" class=\"navbar-link\">Sign Out</a></p>" +
                              $"<p class=\"navbar-text navbar-right\">Signed in as {Session.User.Email}</p>" +
                              "</ul> </div></div></nav>");
        }

        private static void GenerateAllSuggestions()
        {
            PizzaMoreContext context = new PizzaMoreContext();

            using (context)
            {
                IList<Pizza> pizzas = context.PizzaSuggestions.ToList();

                Console.WriteLine("<div class=\"card-deck\">");

                foreach (var pizza in pizzas)
                {
                    Console.WriteLine("<div class=\"card\">");
                    Console.WriteLine(
                        $"<img class=\"card-img-top\" src=\"{pizza.ImageUrl}\" width=\"200px\"alt=\"Card image cap\">");
                    Console.WriteLine("<div class=\"card-block\">");
                    Console.WriteLine($"<h4 class=\"card-title\">{pizza.Title}</h4>");
                    Console.WriteLine(
                        $"<p class=\"card-text\"><a href=\"DetailsPizza.exe?pizzaid={pizza.Id}\">Recipe</a></p>");
                    Console.WriteLine("<form method=\"POST\">");
                    Console.WriteLine(
                        $"<div class=\"radio\"><label><input type = \"radio\" name=\"pizzaVote\" value=\"up\">Up</label></div>");
                    Console.WriteLine(
                        $"<div class=\"radio\"><label><input type = \"radio\" name=\"pizzaVote\" value=\"down\">Down</label></div>");
                    Console.WriteLine($"<input type=\"hidden\" name=\"pizzaid\" value=\"{pizza.Id}\" />");
                    Console.WriteLine("<input type=\"submit\" class=\"btn btn-primary\" value=\"Vote\" />");
                    Console.WriteLine("</form>");
                    Console.WriteLine("</div>");
                    Console.WriteLine("</div>");
                }

                Console.WriteLine("</div>");
            }
        }

        private static void VoteForPizza()
        {
            PostParameters = WebUtil.RetrievePostParameters();
            PizzaMoreContext context = new PizzaMoreContext();

            using (context)
            {
                Pizza pizza = context.PizzaSuggestions.Find(int.Parse(PostParameters["pizzaid"]));
                string vote = PostParameters["pizzaVote"];

                if (vote == "up")
                {
                    pizza.UpVotes++;
                }

                else if (vote == "down")
                {
                    pizza.DownVotes++;
                }

                context.SaveChanges();
            }
        }
    }
}