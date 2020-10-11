using App.ViewModels;
using SUS.HTTP;
using SUS.MvcFramework;
using System;

namespace App.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet("/")]
        public HttpResponse Index()
        {
            var viewModel = new IndexViewModel();

            viewModel.CurrentYear = DateTime.UtcNow.Year;
            viewModel.Message = "Wellcome to Battle Cards 123!";
            if (this.Request.Session.ContainsKey("about"))
            {
                viewModel.Message += "YOU ARE ON THE ABOUTE PAGE!";
            }

            return this.View(viewModel);
        }

        public HttpResponse About()
        {
            this.Request.Session["about"] = "yes";
            return this.View();
        }
    }
}
