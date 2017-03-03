﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShouterApp.Utilities;
using SimpleMVC.Interfaces;

namespace ShouterApp.Views.Home
{
    public class Login : IRenderable
    {
        public string Render()
        {
            string html = HtmlReader.Read(Constans.Login);
            return html;
        }
    }
}
