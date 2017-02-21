namespace SimpleMVC.App.MVC.ViewEngine.Generic
{
    using System;
    using Interfaces.Generic;

    public class ActionResult<T> : IActionResult<T>
    {
        public ActionResult(string viewFullQualifiedName, T model, string location) : this(viewFullQualifiedName, model)
        {
            this.Location = location;
        }

        public ActionResult(string viewFullQualifiedName, T model)
        {
            this.Action =
                (IRenderable<T>) Activator
                    .CreateInstance(Type.GetType(viewFullQualifiedName));

            this.Action.Model = model;
        }

        public IRenderable<T> Action { get; set; }

        public string Invoke()
        {
            return this.Action.Render();
        }

        public string Location { get; }
    }
}
