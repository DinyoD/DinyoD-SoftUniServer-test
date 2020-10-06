using SUS.HTTP;
using SUS.MvcFramework;

namespace AppTest.Controllers
{
    public class UsersController : Controller
    {
        internal HttpResponse Login(HttpRequest request)
        {
            return this.View();
        }

        internal HttpResponse DoLogin(HttpRequest request)
        {
            return this.Redirect("/");
        }

        internal HttpResponse Register(HttpRequest request)
        {
            return this.View();
        }
    }
}
