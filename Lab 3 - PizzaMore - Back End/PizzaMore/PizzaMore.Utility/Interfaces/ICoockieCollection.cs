namespace PizzaMore.Utility.Interfaces
{
    using System.Collections.Generic;

    public interface ICoockieCollection : IEnumerable<Cookie>
    {
        void AddCoockie(Cookie cookie);

        void RemoveCoockie(string cookieName);

        bool ContainsKey(string key);

        int Count { get; }

        Cookie this[string key] { get; set; }
    }
}