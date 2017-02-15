namespace SimpleMVC.App.MVC.Routers
{
    using System;
    using System.Net;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Linq;
    using SimpleHttpServer.Models;
    using SimpleHttpServer.Enums;
    using Controllers;
    using Interfaces;
    using Attributes.Methods;

    public class ControllerRouter : IHandleable
    {
        private IDictionary<string, string> getParams;
        private IDictionary<string, string> postParams;
        private string requestMethod;
        private string controllerName;
        private string actionName;
        private object[] methodParams;

        public HttpResponse Handle(HttpRequest request)
        {
            if (request.Url.Contains("?"))
            {
                RetrieveGetParameters(request);
            }
            else if (!string.IsNullOrEmpty(request.Content))
            {
                RetrievePostParameters(request);
            }

            this.requestMethod = request.Method.ToString();

            this.RetriveControllerAndActionNames(request.Url);

            MethodInfo method = this.GetMethod();

            if (method == null)
            {
                throw new NotSupportedException("No such method");
            }

            IEnumerable<ParameterInfo> parametes = method.GetParameters();

            this.methodParams = new object[parametes.Count()];

            int index = 0;
            foreach (ParameterInfo param in parametes)
            {
                if (param.ParameterType.IsPrimitive)
                {
                    object value = this.getParams[param.Name];
                    this.methodParams[index] = Convert.ChangeType(value, param.ParameterType);
                    index++;
                }
                else
                {
                    Type bindingModelType = param.ParameterType;
                    object bindingModel = Activator.CreateInstance(bindingModelType);
                    IEnumerable<PropertyInfo> properties = bindingModelType.GetProperties();

                    foreach (PropertyInfo property in properties)
                    {
                        property.SetValue(
                            bindingModel, 
                            Convert.ChangeType(
                                postParams[property.Name], 
                                property.PropertyType
                                )
                            );
                    }

                    this.methodParams[index] = Convert.ChangeType(bindingModel, bindingModelType);
                    index++;
                }
            }

            IInvocable actionResult =
                (IInvocable) this.GetMethod()
                    .Invoke(this.GetController(), this.methodParams);

            string content = actionResult.Invoke();
            var response = new HttpResponse()
            {
                StatusCode = ResponseStatusCode.Ok,
                ContentAsUTF8 = content
            };

            return response;
        }

        private MethodInfo GetMethod()
        {
            MethodInfo method = null;

            foreach (MethodInfo methodInfo in this.GetSuitableMethods())
            {
                IEnumerable<Attribute> attributes = methodInfo
                    .GetCustomAttributes()
                    .Where(a => a is HttpMethodAttribute);

                if (!attributes.Any())
                {
                    return methodInfo;
                }

                foreach (HttpMethodAttribute attribute in attributes)
                {
                    if (attribute.IsValid(this.requestMethod))
                    {
                        return methodInfo;
                    }
                }
            }

            return method;
        }

        private IEnumerable<MethodInfo> GetSuitableMethods()
        {
            return this.GetController()
                .GetType()
                .GetMethods()
                .Where(m => m.Name == this.actionName);
        }

        private Controller GetController()
        {
            var controllerType = string.Format(
                "{0}.{1}.{2}",
                MvcContext.Current.AssemblyName,
                MvcContext.Current.ControllersFolder,
                this.controllerName);

            var controller =
                (Controller) Activator.CreateInstance(Type.GetType(controllerType));

            return controller;
        }

        private void RetriveControllerAndActionNames(string url)
        {
            string[] urlInfo = url.Split('/');

            if (urlInfo.Length != 3)
            {
                throw new ArgumentException("Invalid URL");
            }

            string host = urlInfo[0];
            string controller = urlInfo[1];
            string action = urlInfo[2];

            string controllerName = controller[0].ToString().ToUpper() + controller.Substring(1) + "Controller";
            string actionName = action[0].ToString().ToUpper() + action.Substring(1);

            if (actionName.Contains("?"))
            {
                int questionIndex = actionName.IndexOf('?');
                actionName = actionName.Substring(0, questionIndex);
            }

            this.controllerName = controllerName;
            this.actionName = actionName;
        }

        private void RetrievePostParameters(HttpRequest request)
        {
            string contentString = WebUtility.UrlDecode(request.Content);
            postParams = RetrieveParameters(contentString);
        }

        private void RetrieveGetParameters(HttpRequest request)
        {
            int indexOfQuestion = request.Url.IndexOf('?');
            string getParameters = WebUtility.UrlDecode(request.Url.Substring(indexOfQuestion + 1));
            getParams = RetrieveParameters(getParameters);
        }

        private IDictionary<string, string> RetrieveParameters(string parametereString)
        {
            IDictionary<string, string> parametres = new Dictionary<string, string>();

            string decodedParametreString = WebUtility.UrlDecode(parametereString);
            string[] parametresInfo = decodedParametreString.Split('&');

            foreach (string parametre in parametresInfo)
            {
                string[] parametreInfo = parametre.Split('=');
                string parametreName = parametreInfo[0];
                string parametreValue = parametreInfo[1];
                parametres.Add(parametreName, parametreValue);
            }

            return parametres;
        }
    }
}