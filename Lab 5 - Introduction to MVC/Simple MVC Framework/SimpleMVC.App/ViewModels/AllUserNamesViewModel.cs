namespace SimpleMVC.App.ViewModels
{
    using System.Collections.Generic;
    using SimpleMVC.App.MVC.Interfaces;

    public class AllUserNamesViewModel
    {
        public IList<string> UserNames { get; set; }
    }
}
