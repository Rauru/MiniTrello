using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiniTrello.Api.Controllers
{
    //[Post/put("boards/addmember/{accesstoken}")]
    //httpsresponcemessage
    public class BoardController : Controller
    {
        //
        // GET: /Board/

        public ActionResult Index()
        {
            return View();
        }

    }
}
