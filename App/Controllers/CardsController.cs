using SUS.HTTP;
using SUS.MvcFramework;

namespace AppTest.Controllers
{
    public class CardsController : Controller
    {
        internal HttpResponse All(HttpRequest request)
        {
            return this.View();
        }

        internal HttpResponse Collection(HttpRequest request)
        {
            return this.View();
        }

        internal HttpResponse Add(HttpRequest request)
        {
            return this.View();
        }
    }
}
