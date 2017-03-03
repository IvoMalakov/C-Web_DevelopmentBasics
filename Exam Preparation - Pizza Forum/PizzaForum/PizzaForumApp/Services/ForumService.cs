using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using PizzaForumApp.BindingModels;
using PizzaForumApp.Data;
using AutoMapper;
using PizzaForumApp.Models;
using SimpleHttpServer.Models;
using SimpleHttpServer.Utilities;
using Cookie = SimpleHttpServer.Models.Cookie;

namespace PizzaForumApp.Services
{
    public class ForumService : Service
    {
        public ForumService(PizzaForumContext context) : base(context)
        {
        }

        public bool RegisterUser(RegisterUserBindingModel model)
        {
            if (model.Password == model.ConfirmPassword)
            {
                User checkedUser =
                    this.Context.Users.FirstOrDefault(u => u.Username == model.Username && u.Email == model.Email);

                if (checkedUser == null)
                {
                    ConfigureMapper();
                    User user = Mapper.Map<User>(model);
                    int userConter = this.Context.Users.Count();

                    user.IsAdmin = userConter == 0;

                    this.Context.Users.Add(user);
                    this.Context.SaveChanges();
                    return true;
                }

                return false;
            }

            return false;
        }

        public bool LoginUser(LoginUserBindingModel model, HttpSession session)
        {
            User userFromDb =
                this.Context.Users.FirstOrDefault(
                    (u =>
                        u.Password == model.Password &&
                        (u.Username == model.UsernameOrEmail || u.Email == model.UsernameOrEmail)));

            if (userFromDb != null)
            {
                Login login = new Login()
                {
                    IsActive = true,
                    SessionId = session.Id,
                    User = userFromDb
                };

                this.Context.Logins.Add(login);
                this.Context.SaveChanges();

                return true;
            }

            return false;
        }

        public void LogoutUser(HttpResponse response, string sessionId)
        {
            Login login = this.Context.Logins.FirstOrDefault(l => l.IsActive && l.SessionId == sessionId);
            login.IsActive = false;
            this.Context.SaveChanges();

            HttpSession session = SessionCreator.Create();
            Cookie sessionCookie = new Cookie("sessionId", session.Id + "; HttpOnly; path=/");
            response.Header.AddCookie(sessionCookie);
        }

        private void ConfigureMapper()
        {
            Mapper.Initialize(expression => expression.CreateMap<RegisterUserBindingModel, User>());
        }
    }
}
