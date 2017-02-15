namespace SimpleMVC.App.MVC.ViewEngine
{
    using System;
    using Interfaces;

    public class ActionResult : IActionResult
    {
        public ActionResult(string viewFullQualifiedName)
        {
            this.Action = (IReanderable) Activator
                .CreateInstance(Type.GetType(viewFullQualifiedName));
        }

        public IReanderable Action { get; set; }

        public string Invoke()
        {
            return this.Action.Render();
        }
    }
}
