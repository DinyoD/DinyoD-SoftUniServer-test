﻿using SUS.HTTP;
using SUS.MvcFramework;

namespace App.Controllers
{
    public class CardsController : Controller
    {
        public HttpResponse All()
        {
            return this.View();
        }

        public HttpResponse Collection()
        {
            return this.View();
        }

        public HttpResponse Add()
        {
            return this.View();
        }
    }
}
