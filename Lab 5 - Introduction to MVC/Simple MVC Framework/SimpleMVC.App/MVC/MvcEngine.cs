﻿namespace SimpleMVC.App.MVC
{
    using System;
    using System.Reflection;
    using SimpleHttpServer;

    public static class MvcEngine
    {
        public static void Run(HttpServer server)
        {
            RegisterAssemblyName();
            RigisterControllers();
            RegisterViews();
            RegisterModels();

            try
            {
                server.Listen();
            }
            catch (Exception e)
            {
                //Log errors
                Console.WriteLine(e.Message);
            }
        }

        private static void RegisterModels()
        {
            MvcContext.Current.ModelsFolder = "Models";
        }

        private static void RegisterViews()
        {
            MvcContext.Current.ViewsFolder = "Views";
        }

        private static void RigisterControllers()
        {
            MvcContext.Current.ControllersFolder = "Controllers";
            MvcContext.Current.ControllersSuffix = "Controller";
        }

        private static void RegisterAssemblyName()
        {
            MvcContext.Current.AssemblyName =
                Assembly.GetExecutingAssembly().GetName().Name;
        }
    }
}
