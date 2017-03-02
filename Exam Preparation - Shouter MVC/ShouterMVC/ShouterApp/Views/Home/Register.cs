using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShouterApp.BindingModels;
using ShouterApp.Utilities;
using SimpleMVC.Interfaces.Generic;

namespace ShouterApp.Views.Home
{
    public class Register : IRenderable<RegisterUserBindingModel>
    {
        public string Render()
        {
            string html = HtmlReader.Read(Constans.Register);
            return html;
        }

        public RegisterUserBindingModel Model { get; set; }
    }
}
