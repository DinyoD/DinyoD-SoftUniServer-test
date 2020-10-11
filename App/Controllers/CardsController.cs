using App.Data;
using App.ViewModels;
using SUS.HTTP;
using SUS.MvcFramework;
using System.Linq;

namespace App.Controllers
{
    public class CardsController : Controller
    {
        public HttpResponse All()
        {
            var db = new ApplicationDbContext();
            var cardsViewModel = db.Cards.Select(x => new CardViewModel
            {
                Name = x.Name,
                Description = x.Description,
                Health = x.Health,
                Attack = x.Attack,
                Type = x.Keyword,
                ImageUrl = x.ImageUrl,
            }).ToList();
            return this.View(new AllCardsViewModel { Cards = cardsViewModel });
        }

        public HttpResponse Collection()
        {
            return this.View();
        }

        public HttpResponse Add()
        {
            return this.View();
        }


        [HttpPost("/Cards/Add")]
        public HttpResponse DoAdd()
        {

            var db = new ApplicationDbContext();

            if (this.Request.FormData["name"].Length < 5)
            {
                return this.Error("Name should be at least 5 characters long.");
            }

            db.Cards.Add(new Card
            {
                Attack = int.Parse(this.Request.FormData["attack"]),
                Health = int.Parse(this.Request.FormData["health"]),
                Description = this.Request.FormData["description"],
                Name = this.Request.FormData["name"],
                ImageUrl = this.Request.FormData["image"],
                Keyword = this.Request.FormData["keyword"],
            });

            db.SaveChanges();

            return this.Redirect("/");
        }
    }
}
