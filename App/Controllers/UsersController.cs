using SUS.HTTP;
using SUS.MvcFramework;

namespace AppTest.Controllers
{
    public class UsersController : Controller
    {
        public HttpResponse Login()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse DoLogin()
        {
            return this.Redirect("/");
        }

        public HttpResponse Register()
        {
            return this.View();
        }
    }
}
