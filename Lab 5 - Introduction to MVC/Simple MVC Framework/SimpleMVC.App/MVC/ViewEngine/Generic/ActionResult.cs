namespace SimpleMVC.App.MVC.ViewEngine.Generic
{
    using System;
    using Interfaces.Generic;

    public class ActionResult<T> : IActionResult<T>
    {
        public ActionResult(string viewFullQualifiedName, T model) : this(viewFullQualifiedName, model, string.Empty)
        {
            
        }

        public ActionResult(string viewFullQualifiedName, T model, string location)
        {
            this.Action =
                (IRenderable<T>) Activator
                    .CreateInstance(Type.GetType(viewFullQualifiedName));

            this.Action.Model = model;
            this.Location = location;
        }

        public IRenderable<T> Action { get; set; }

        public string Invoke()
        {
            return this.Action.Render();
        }

        public string Location { get; }
    }
}
