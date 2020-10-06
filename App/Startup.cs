using System;
using System.Collections.Generic;
using AppTest.Controllers;
using SUS.HTTP;
using SUS.MvcFramework;

namespace AppTest
{
    public class Startup : IMvcAplication
    {
        public void ConfigureServices()
        {
        }

        public void Configure(List<Route> routeTable)
        {

            routeTable.Add(new Route("/", HttpMethod.Get, new HomeController().Index));


            routeTable.Add(new Route("/css/bootstrap.min.css", HttpMethod.Get, new StaticFilesController().BootstrapCss));
            routeTable.Add(new Route("/css/custom.css", HttpMethod.Get, new StaticFilesController().CustomCss));
            routeTable.Add(new Route("/js/bootstrap.bundle.min.js", HttpMethod.Get, new StaticFilesController().BootstrapJs));
            routeTable.Add(new Route("/js/custom.js", HttpMethod.Get, new StaticFilesController().CustomJs));
            routeTable.Add(new Route("favicon.ico", HttpMethod.Get, new StaticFilesController().Favicon));

        }

    }
}
