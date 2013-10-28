using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using TicketRewardSystem.Models;

namespace TicketRewardSystem.ViewModels
{
    public class AchievementViewModel
    {
        public int AchievementId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public static Expression<Func<Achievement, AchievementViewModel>> FromAchievement
        {
            get
            {
                return ach => new AchievementViewModel
                {
                    AchievementId = ach.AchievementId,
                    Title = ach.Title,
                    ImageUrl = ach.ImageUrl
                };
            }
        }
    }
}