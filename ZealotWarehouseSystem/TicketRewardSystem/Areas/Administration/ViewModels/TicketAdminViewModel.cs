using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using TicketRewardSystem.Models;

namespace TicketRewardSystem.Areas.Administration.ViewModels
{
    public class TicketAdminViewModel
    {
        public static Expression<Func<Ticket, TicketAdminViewModel>> FromTicket
        {
            get
            {
                return t => new TicketAdminViewModel
                {
                    Id = t.TicketId,
                    Title = t.Title,
                    Description = t.Description,
                    PostedBy = t.PostedBy.UserName,
                    PostedOn = t.PostedOn,
                    AssignedTo = t.AssignedTo.UserName,
                    Priority=t.Priority,
                    Status = t.Status
                };
            }
        }

        //[ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        public string Title { get; set; }

        public string Description { get; set; }

        public string PostedBy { get; set; }

        //[DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode=true,DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime PostedOn { set; get; }

        //[DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<DateTime> ResolvedOn { set; get; }

        public string AssignedTo { set; get; }

        [UIHint("Priority")]
        public PriorityEnum Priority { set; get; }

        [UIHint("Status")]
        public StatusEnum Status { set; get; }
    }
}