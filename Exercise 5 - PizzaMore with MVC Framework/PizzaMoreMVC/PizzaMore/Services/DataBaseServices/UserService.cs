using SimpleHttpServer.Models;

namespace PizzaMore.Services.DataBaseServices
{
    using System.Linq;
    using PizzaMore.Utilities;
    using PizzaMore.Data;
    using PizzaMore.Models;
    using PizzaMore.BindingModels;

    public class UserService : DataBaseService
    {
        public UserService(PizzaMoreContext context) : base(context)
        {

        }

        public void RegisterUser(UserBindingModel model)
        {
            User user = new User()
            {
                Email = model.email,
                Password = PasswordHasher.Hash(model.password)
            };

            this.Context.Users.Add(user);
            this.Context.SaveChanges();
        }

        public User LoginUser(UserBindingModel model, HttpSession session)
        {
            string hashedPassword = PasswordHasher.Hash(model.password);

            User user =
                this.Context.Users.SingleOrDefault(u => u.Email == model.email);

            if (hashedPassword == user.Password)
            {
                return user;
            }

            return null;
        }
    }
}
