using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TicketRewardSystem.Areas.Administration.Controllers
{
    public class HomeAdminController : AdminBaseController
    {
        //
        // GET: /Administration/Home/
        public ActionResult Index()
        {
            return View();
        }
	}
}