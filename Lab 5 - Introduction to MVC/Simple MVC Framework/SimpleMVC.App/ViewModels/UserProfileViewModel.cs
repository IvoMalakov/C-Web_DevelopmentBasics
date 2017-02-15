namespace SimpleMVC.App.ViewModels
{
    using System.Collections.Generic;
    using BindingModels;

    public class UserProfileViewModel
    {
        public string UserName { get; set; }

        public int UserId { get; set; }

        public IEnumerable<NoteViewModel> Notes { get; set; }
    }
}
