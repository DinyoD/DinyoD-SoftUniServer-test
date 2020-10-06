using SUS.HTTP;
using System.Collections.Generic;

namespace SUS.MvcFramework
{
    public interface IMvcAplication
    {
        void ConfigureServices();

        void Configure(List<Route>routeTable);
    }
}
