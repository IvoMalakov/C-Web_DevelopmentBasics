namespace SimpleMVC.App.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MVC.Interfaces;
    using MVC.Interfaces.Generic;
    using MVC.Attributes.Methods;
    using MVC.Security;
    using ViewModels;
    using BindingModels;
    using Data;
    using Data.Models;
    using SimpleHttpServer.Models;
    using SimpleHttpServer.Utilities;

    public class UsersController : Controller
    {
        private SignInManager signInManager;

        public UsersController()
        {
            signInManager = new SignInManager(new NotesAppContext());
        }

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
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginUserBindingModel model, HttpSession session)
        {
            string userName = model.Username;
            string paswword = model.Password;
            string sessionId = session.Id;

            using (var context = new NotesAppContext())
            {
                var user = context.Users.FirstOrDefault(u => u.UserName == userName && u.Password == paswword);

                if (user != null)
                {
                    Login login = new Login()
                    {
                        IsActive = true,
                        UserId = user.Id,
                        User = user,
                        SessionId = sessionId
                    };

                    context.Logins.Add(login);
                    context.SaveChanges();
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult<AllUserNamesViewModel> All(HttpSession session)
        {
            if (!signInManager.IsAuthenticated(session))
            {
                return Redirect(new AllUserNamesViewModel(), "users/login");
            }

            else
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