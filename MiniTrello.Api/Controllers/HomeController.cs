﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AttributeRouting.Web.Http;

namespace MiniTrello.Api.Controllers
{
    public class HomeController : Controller
    {
        [GET("")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
