using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using TicketRewardSystem.Areas.Administration.ViewModels;
using TicketRewardSystem.Repository;
using TicketRewardSystem.Models;

namespace TicketRewardSystem.Areas.Administration.Controllers
{
    public class TicketsAdminController : AdminBaseController
    {

        protected IUowData Data { set; get; }
        public TicketsAdminController()
        {
            this.Data = new UowData();
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ReadTickets([DataSourceRequest]DataSourceRequest request)
        {
            var tickets = this.Data.Tickets.All().Select(TicketAdminViewModel.FromTicket);

            DataSourceResult result= tickets.ToDataSourceResult(request);
            return Json(result,JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateTicket([DataSourceRequest] DataSourceRequest request, TicketAdminViewModel ticket)
        {
            var existingTicket = this.Data.Tickets.GetById(ticket.Id);

            //var xa = ModelState.Keys["PostedOn"];

            if (ModelState.ContainsKey("PostedOn"))
                ModelState["PostedOn"].Errors.Clear();


            if (ticket != null && ModelState.IsValid)
            {
                //var xah = this.Data.Users.All().FirstOrDefault(x => x.UserName == ticket.PostedBy);
                existingTicket.Title = ticket.Title;
                existingTicket.Description = ticket.Description;
                existingTicket.PostedBy = this.Data.Users.All().FirstOrDefault(x => x.UserName == ticket.PostedBy);
                existingTicket.Status = ticket.Status;
                existingTicket.Priority = ticket.Priority;
                //existingTicket.AssignedTo = this.Data.Users.All().FirstOrDefault(x => x.UserName == ticket.AssignedTo);
                ticket.ResolvedOn = existingTicket.ResolvedOn;
                ticket.PostedOn = existingTicket.PostedOn;

                if (!String.IsNullOrEmpty(ticket.AssignedTo))
                {
                    existingTicket.AssignedTo = this.Data.Users.All().FirstOrDefault(x => x.UserName == ticket.AssignedTo);
                }
                else
                {
                    existingTicket.AssignedTo = null;
                }
                this.Data.SaveChanges();
            }

           

            return Json((new[] { ticket }.ToDataSourceResult(request, ModelState)), JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteTicket([DataSourceRequest] DataSourceRequest request, TicketAdminViewModel ticket)
        {
            var existingTicket = this.Data.Tickets.GetById(ticket.Id);

            if (existingTicket != null)
            {
                this.Data.Tickets.Delete(existingTicket.TicketId);
                this.Data.SaveChanges();
            }

            return Json(new[] { ticket }, JsonRequestBehavior.AllowGet);
        }

       
    }
}
