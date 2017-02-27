namespace PizzaMore.Services.DataBaseServices
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using PizzaMore.Data;
    using PizzaMore.ViewModels;
    using PizzaMore.Models;
    using PizzaMore.BindingModels;
    using SimpleHttpServer.Models;


    public class PizzaService : DataBaseService
    {
        public PizzaService(PizzaMoreContext context) : base(context)
        {
        }

        public IEnumerable<PizzaViewModel> GetPizzas(HttpSession session, string sortedMethod)
        {
            var pizzasVms = new List<PizzaViewModel>();

            var pizzas = this.Context.Pizzas.ToList();
            IEnumerable<Pizza> sortedPizzas = this.GetSortedPizzas(pizzas, sortedMethod);

            foreach (var pizza in sortedPizzas)
            {

                var pizzaVm = new PizzaViewModel()
                {
                    Id = pizza.Id,
                    ImageUrl = pizza.ImageUrl,
                    Title = pizza.Title,
                    UpVotes = pizza.UpVotes,
                    DownVotes = pizza.DownVotes,
                    Owner = this.Context.Sessions.FirstOrDefault(s => s.Id == session.Id).User
                };

                pizzasVms.Add(pizzaVm);
            }

            return pizzasVms;
        }

        public void VoteForPizza(int pizzaId, string voteType, HttpSession session)
        {

            Pizza votePizza = this.Context.Pizzas.Find(pizzaId);

            switch (voteType.ToLower())
            {
                case "up":
                    votePizza.UpVotes++;
                    break;

                case "down":
                    votePizza.DownVotes++;
                    break;

                default:
                    throw new ArgumentException("Error! Invalid vote type. Vote type must be \"up\" or \"down\"");
            }

            this.Context.SaveChanges();
        }

        public void AddPizza(HttpSession session, PizzaBindingModel model)
        {
            User user = this.Context.Sessions.FirstOrDefault(s => s.Id == session.Id).User;

            ConfigureMapper(session);
            Pizza pizzaEntity = Mapper.Map<Pizza>(model);

            user.Suggestions.Add(pizzaEntity);
            this.Context.SaveChanges();
        }

        private void ConfigureMapper(HttpSession session)
        {
            Mapper.Initialize(expression => expression.CreateMap<PizzaBindingModel, Pizza>()
                .ForMember(p => p.ImageUrl, config => config
                    .MapFrom(m => m.url))
                .ForMember(p => p.Owner, config => config
                    .MapFrom(u => this.Context.Sessions.FirstOrDefault(s => s.Id == session.Id).User)));
        }

        private IEnumerable<Pizza> GetSortedPizzas(List<Pizza> pizzas, string sortedMethod)
        {
            switch (sortedMethod)
            {
                case "defaultSort":
                    return pizzas.OrderByDescending(p => p.UpVotes).ThenBy(p => p.DownVotes).AsEnumerable();
                case "names_upvotes":
                    return pizzas.OrderBy(p => p.Title).ThenBy(p => p.UpVotes).AsEnumerable();
                case "names_downvotes":
                    return pizzas.OrderBy(p => p.Title).ThenBy(p => p.DownVotes).AsEnumerable();
                case "upvotes_names":
                    return pizzas.OrderBy(p => p.UpVotes).ThenBy(p => p.Title).AsEnumerable();
                case "upvotes_downvotes":
                    return pizzas.OrderBy(p => p.UpVotes).ThenBy(p => p.DownVotes).AsEnumerable();
                case "downvotes_names":
                    return pizzas.OrderBy(p => p.DownVotes).ThenBy(p => p.Title).AsEnumerable();
                case "downvotes_upvotes":
                    return pizzas.OrderBy(p => p.DownVotes).ThenBy(p => p.UpVotes).AsEnumerable();
                case "names_names":
                    return pizzas.OrderBy(p => p.Title).AsEnumerable();
                case "downvotes_downvotes":
                    return pizzas.OrderBy(p => p.DownVotes).AsEnumerable();
                case "upvotes_upvotes":
                    return pizzas.OrderBy(p => p.UpVotes).AsEnumerable();
                default:
                    return pizzas;
            }
        }
    }
}
