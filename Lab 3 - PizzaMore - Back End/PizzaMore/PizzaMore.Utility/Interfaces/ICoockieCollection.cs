namespace PizzaMore.Utility.Interfaces
{
    public interface ICoockieCollection
    {
        void AddCoockie(Cookie cookie);

        void RemoveCoockie(string cookieName);

        bool ContainsKey(string key);

        int Count { get; }

        Cookie this[string key] { get; set; }
    }
}
