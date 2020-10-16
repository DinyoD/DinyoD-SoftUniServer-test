using SUS.HTTP;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SUS.MvcFramework
{
    public static class Host
    {
        public static async Task CreateHostAsync(IMvcAplication app, int port =80)
        {
            List<Route> routeTable = new List<Route>();
            IServiceCollection serviceCollection = new ServiceCollection();

            app.Configure(routeTable);
            app.ConfigureServices(serviceCollection);

            AutoRegisterStaticFile(routeTable);
            AutoRegisterRoutes(routeTable, app, serviceCollection);


            IHttpServer server = new HttpServer(routeTable);
            await server.StartAsync(port);
        }

        private static void AutoRegisterRoutes(List<Route> routeTable, IMvcAplication app, IServiceCollection serviceCollection)
        {
            var controllerTypes = app.GetType().Assembly.GetTypes()
                .Where(x=>x.IsClass && !x.IsAbstract && x.IsSubclassOf(typeof(Controller)));

            foreach (var controllerType in controllerTypes)
            {
                var methods = controllerType.GetMethods()
                    .Where(x=>x.IsPublic && x.DeclaringType == controllerType 
                    && !x.IsStatic && !x.IsAbstract && !x.IsConstructor && !x.IsSpecialName);

                foreach (var method in methods)
                {
                    var url = "/" + controllerType.Name.Replace("Controller", string.Empty) + "/" + method.Name;

                    var attribute = method.GetCustomAttributes(false)
                        .Where(x => x.GetType().IsSubclassOf(typeof(BaseHttpAttribute)))
                        .FirstOrDefault() as BaseHttpAttribute;

                    var httpMethod = HttpMethod.Get;

                    if (attribute != null)
                    {
                        httpMethod = attribute.Method;
                    }

                    if (!string.IsNullOrEmpty(attribute?.Url))
                    {
                        url = attribute.Url;
                    }

                    routeTable.Add(new Route(url, httpMethod, (request) 
                                                       =>ExecuteAction(request, controllerType, method, serviceCollection)));
                }
            }
        }

        private static HttpResponse ExecuteAction(HttpRequest request, Type controllerType, MethodInfo action, IServiceCollection serviceCollection)
        {

            var instance = serviceCollection.CreateInstance(controllerType) as Controller;
            instance.Request = request;
            var arguments = new List<object>();
            var parameters = action.GetParameters();

            foreach (var parameter in parameters)
            {
                var httpParamerValue = GetParameterFromRequest(request, parameter.Name);
                var parameterValue = Convert.ChangeType(httpParamerValue, parameter.ParameterType);
                if (parameterValue == null 
                    && parameter.ParameterType != typeof(string) 
                    && parameter.ParameterType != typeof(int?))
                {

                    parameterValue = Activator.CreateInstance(parameter.ParameterType);
                    var properties = parameter.ParameterType.GetProperties();

                    foreach (var property in properties)
                    {
                        var propertyHttpParamValue = GetParameterFromRequest(request, property.Name);
                        var propertyParamValue = Convert.ChangeType(propertyHttpParamValue, property.PropertyType);

                        property.SetValue(parameterValue, propertyParamValue);
                    }
                }

                arguments.Add(parameterValue);
            }

            var response = action.Invoke(instance, arguments.ToArray()) as HttpResponse;
            return response;
        }

        private static object GetParameterFromRequest(HttpRequest request, string name)
        {
            var parameterName = name.ToLower();
            if (request.FormData.Any(x=>x.Key.ToLower() == parameterName))
            {
                return request.FormData.FirstOrDefault(x => x.Key.ToLower() == parameterName).Value;
            }
            if (request.QueryData.Any(x=>x.Key.ToLower() == parameterName))
            {
                return request.QueryData.FirstOrDefault(x => x.Key.ToLower() == parameterName).Value;
            }
            return null;
        }

        private static void AutoRegisterStaticFile(List<Route> routeTable)
        {
            var staticFiles = Directory.GetFiles("wwwroot", "*",SearchOption.AllDirectories);

            foreach (var staticFile in staticFiles)
            {
                var url = staticFile.Replace("wwwroot", string.Empty).Replace("\\", "/");

                routeTable.Add(new Route(url, HttpMethod.Get, (request) => 
                {
                    var fileContent = File.ReadAllBytes(staticFile);
                    var fileExt = new FileInfo(staticFile).Extension;

                    var contentType = fileExt switch
                    {
                        ".txt" => "text/plain",
                        ".js" => "text/javascript",
                        ".css" => "text/css",
                        ".jpg" => "image/jpg",
                        ".jpeg" => "image/jpg",
                        ".png" => "image/png",
                        ".gif" => "image/gif",
                        ".ico" => "image/vnd.microsoft.icon",
                        ".html" => "text/html",
                        _ => "text/plain",
                    };

                    return new HttpResponse(contentType,fileContent, HttpStatusCode.Ok);
                }));
            }
        }
    }
}


