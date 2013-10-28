using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TicketRewardSystem.Areas.Administration.Controllers
{
    [Authorize(Roles="admin")]
    public abstract class AdminBaseController : Controller
    {
        
	}
}