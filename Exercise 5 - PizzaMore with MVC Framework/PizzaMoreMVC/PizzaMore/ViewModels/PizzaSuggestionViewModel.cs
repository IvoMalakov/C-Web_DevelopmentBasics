namespace PizzaMore.ViewModels
{
    using System.Collections.Generic;
    using PizzaMore.Models;

    public class PizzaSuggestionViewModel
    {
        public string Email { get; set; }

        public ICollection<Pizza> PizzaSuggestions { get; set; }
    }
}
