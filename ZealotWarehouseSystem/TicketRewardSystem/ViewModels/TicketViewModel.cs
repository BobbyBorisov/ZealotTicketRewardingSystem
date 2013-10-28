using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using TicketRewardSystem.Models;

namespace TicketRewardSystem.ViewModels
{
    public class TicketViewModel
    {
        public static Expression<Func<Ticket, TicketViewModel>> FromTicket
        {
            get
            {
                return ticket => new TicketViewModel
                {   
                    TicketId = ticket.TicketId,
                    Title = ticket.Title,
                    Description = ticket.Description,
                    PostedOn = ticket.PostedOn,
                    PostedBy = ticket.PostedBy.UserName,
                    AssignedTo = ticket.AssignedTo.UserName,
                    Status = ticket.Status
                };
            }
        }
        public int TicketId { get; set; }

        public string Title { get; set; }

        [AllowHtml]
        public string Description { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy H:mm}")]
        public DateTime PostedOn { get; set; }


        public string PostedBy { get; set; }

        public string Priority { get; set; }

        public StatusEnum Status { get; set; }

        // TODO: on support controller, we'll have to choose if we'll use home/readAlltickets action from home, as we need the assignedTo field, but don't need it in home..
        public string AssignedTo { get; set; }
    }
}
