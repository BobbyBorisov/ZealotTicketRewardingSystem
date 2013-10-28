using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TicketRewardSystem.Models
{
    public class Achievement
    {
        public int AchievementId { get; set; }

        [Required]
        [StringLength(100)]        
        public string Title { get; set; }

        [Required]
        [StringLength(500)]
        public string Text { get; set; }

        [Required]
        [StringLength(500)]
        public string ImageUrl { get; set; }
        public virtual ICollection<ApplicationUser> Owners { get; set; }

        public Achievement()
        {
            this.Owners = new HashSet<ApplicationUser>();
        }
    }
}