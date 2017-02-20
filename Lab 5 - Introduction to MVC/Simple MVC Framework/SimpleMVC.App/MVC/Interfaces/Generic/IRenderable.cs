namespace SimpleMVC.App.MVC.Interfaces.Generic
{
    public interface IRenderable<T> :  IReanderable
    {
        T Model { get; set; }
    }
}
