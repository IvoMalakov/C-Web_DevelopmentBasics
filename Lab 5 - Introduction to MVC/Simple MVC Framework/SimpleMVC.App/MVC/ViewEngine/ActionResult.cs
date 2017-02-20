namespace SimpleMVC.App.MVC.ViewEngine
{
    using System;
    using Interfaces;

    public class ActionResult : IActionResult
    {
        public ActionResult(string viewFullQualifiedName) : this(viewFullQualifiedName, string.Empty)
        {
            
        }

        public ActionResult(string viewFullQualifiedName, string location)
        {
            this.Action = (IReanderable) Activator
                .CreateInstance(Type.GetType(viewFullQualifiedName));

            this.Location = location;
        }

        public IReanderable Action { get; set; }

        public string Invoke()
        {
            return this.Action.Render();
        }

        public string Location { get; }
    }
}
