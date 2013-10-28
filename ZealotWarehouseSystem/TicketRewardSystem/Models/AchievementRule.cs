using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TicketRewardSystem.Models
{
    public class AchievementRule
    {
        public int AchievementRuleId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public Nullable<int> TimespanDays { get; set; }
        public PriorityEnum Priority { get; set; }

        [Range(1, 1000)]
        public int TicketsCount { get; set; }
        public virtual Achievement Achievement { get; set; }
    }
}