using SUS.HTTP;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace SUS.MvcFramework
{
    public abstract class Controller
    {
        public HttpResponse View([CallerMemberName]string viewPath = null)
        {
            var layout = System.IO.File.ReadAllText("Views/Shared/_Layout.cshtml");

            var viewContent = System.IO.File.ReadAllText("Views/" + this.GetType().Name.Replace("Controller", string.Empty) + "/" + viewPath + ".cshtml");

            var responseHtml = layout.Replace("@RenderBody()", viewContent);

            var responseBodyBytes = Encoding.UTF8.GetBytes(responseHtml);
            var response = new HttpResponse("text/html", responseBodyBytes);

            return response;
        }


        public HttpResponse File(string filePath, string contentType)
        {
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            var response = new HttpResponse(contentType, fileBytes);

            return response;
        }

        public HttpResponse Redirect (string url)
        {
            var response = new HttpResponse(HttpStatusCode.Found);
            response.Headers.Add(new Header("Location", url));

            return response;
        }



        //static HttpResponse HomePage(HttpRequest request)
        //{
        //    var responseHtml = "<h1>Home Page</h1>";
        //    var responseBodyBytes = Encoding.UTF8.GetBytes(responseHtml);

        //    var response = new HttpResponse("text/html", responseBodyBytes);
        //    return response;
        //}

        //static HttpResponse About(HttpRequest request)
        //{
        //    var responseHtml = "<h1>About..</h1>";

        //    var responseBodyBytes = Encoding.UTF8.GetBytes(responseHtml);

        //    var response = new HttpResponse("text/html", responseBodyBytes);

        //    return response;
        //}

        //static HttpResponse Favicon(HttpRequest request)
        //{
        //    var fileBytes = System.IO.File.ReadAllBytes("wwwroot/favicon.ico");

        //    var response = new HttpResponse("image/vnd.microsoft.icon", fileBytes);

        //    return response;
        //}
    }
}
