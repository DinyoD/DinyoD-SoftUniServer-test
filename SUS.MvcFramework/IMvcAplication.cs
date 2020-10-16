using SUS.HTTP;
using System.Collections.Generic;

namespace SUS.MvcFramework
{
    public interface IMvcAplication
    {
        void ConfigureServices(IServiceCollection serviceCollection);

        void Configure(List<Route>routeTable);
    }
}
