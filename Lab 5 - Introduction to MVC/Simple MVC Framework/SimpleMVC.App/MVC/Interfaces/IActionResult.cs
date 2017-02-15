namespace SimpleMVC.App.MVC.Interfaces
{
    public interface IActionResult : IInvocable
    {
        IReanderable Action { get; set; }
    }
}
