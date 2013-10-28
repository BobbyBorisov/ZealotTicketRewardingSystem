using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using TicketRewardSystem.Models;

namespace TicketRewardSystem.ViewModels
{
    public class RuleViewModel
    {
        public int AchievementRuleId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public int? TimespanDays { get; set; }
        public PriorityEnum Priority { get; set; }
        
        public int TicketsCount { get; set; }

        [Required]
        public int Achievement { get; set; }

        public static Expression<Func<AchievementRule, RuleViewModel>> FromRule
        {
            get
            {
                return rule => new RuleViewModel
                {
                    AchievementRuleId = rule.AchievementRuleId,
                    Title = rule.Title,
                    TicketsCount = rule.TicketsCount,
                    TimespanDays = rule.TimespanDays,
                    Priority = rule.Priority,
                    Achievement = rule.Achievement.AchievementId
                };
            }
        }
    }
}