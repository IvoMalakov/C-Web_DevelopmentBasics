namespace SimpleMVC.App.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using MVC.Interfaces;
    using MVC.Interfaces.Generic;
    using MVC.Attributes.Methods;
    using ViewModels;
    using BindingModels;
    using Data;
    using Data.Models;

    public class UsersController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterUserBindingModel model)
        {
            var user = new User()
            {
                UserName = model.Username,
                Password = model.Password
            };

            using (var context = new NotesAppContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }

            return View();
        }

        [HttpGet]
        public IActionResult<AllUserNamesViewModel> All()
        {
            List<string> usernames = null;

            using (var context = new NotesAppContext())
            {
                usernames = context.Users.Select(u => u.UserName).ToList();
            }

            var viewModel = new AllUserNamesViewModel()
            {
                UserNames = usernames
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult<UserProfileViewModel> Profile(int id)
        {
            using (var context = new NotesAppContext())
            {
                var user = context.Users.Find(id);
                var viewModel = new UserProfileViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    Notes = user.Notes
                        .Select(x =>
                            new NoteViewModel()
                            {
                                Title = x.Title,
                                Content = x.Content
                            }
                        )
                };

                return View(viewModel);
            }
        }

        [HttpPost]
        public IActionResult<UserProfileViewModel> Profile(AddNoteBindingModel model)
        {
            using (var context = new NotesAppContext())
            {
                var user = context.Users.Find(model.UserId);
                var note = new Note
                {
                    Title = model.Title,
                    Content = model.Content
                };

                user.Notes.Add(note);
                context.SaveChanges();
            }

            return Profile(model.UserId);
        }
    }
}