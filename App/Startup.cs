using System.Collections.Generic;
using App.Data;
using Microsoft.EntityFrameworkCore;
using SUS.HTTP;
using SUS.MvcFramework;

namespace App
{
    public class Startup : IMvcAplication
    {
        public void ConfigureServices(IServiceCollection serviceCollection)
        {

        }

        public void Configure(List<Route> routeTable)
        {
            new ApplicationDbContext().Database.Migrate();
        }

    }
}
