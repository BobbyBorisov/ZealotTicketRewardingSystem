using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TicketRewardSystem.Models;
using TicketRewardSystem.Repository;
using TicketRewardSystem.ViewModels;

namespace TicketRewardSystem.Areas.Administration.Controllers
{
    public class RulesController : AdminBaseController
    {
        private UowData db = new UowData();

        // GET: /Administration/Rules/
        public ActionResult Index()
        {
            return View(db.Rules.All().Include("Achievement").ToList());
        }

        // GET: /Administration/Rules/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AchievementRule achievementrule = db.Rules.GetById((int)id);
            if (achievementrule == null)
            {
                return HttpNotFound();
            }
            return View(achievementrule);
        }

        // GET: /Administration/Rules/Create
        public ActionResult Create()
        {
            var achievements = this.db.Achievements.All();
            return View();
        }

        // POST: /Administration/Rules/Create
		// To protect from over posting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		// 
		// Example: public ActionResult Update([Bind(Include="ExampleProperty1,ExampleProperty2")] Model model)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RuleViewModel achievementrule)
        {           
            if (achievementrule != null && ModelState.IsValid)
            {
                AchievementRule newRule = new AchievementRule()
                {
                    AchievementRuleId = achievementrule.AchievementRuleId,
                    Title = achievementrule.Title,
                    TicketsCount = achievementrule.TicketsCount,
                    TimespanDays = achievementrule.TimespanDays,
                    Priority = achievementrule.Priority                    
                };
                var existingAch = db.Achievements.GetById((int)achievementrule.Achievement);
                newRule.Achievement = existingAch;

                db.Rules.Add(newRule);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(achievementrule);
        }

        // GET: /Administration/Rules/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AchievementRule achievementrule = db.Rules.GetById((int)id);
            if (achievementrule == null)
            {
                return HttpNotFound();
            }
            RuleViewModel model = ConvertToViewModel(achievementrule);
            
            return View(model);
        }

       
        // POST: /Administration/Rules/Edit/5
		// To protect from over posting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		// 
		// Example: public ActionResult Update([Bind(Include="ExampleProperty1,ExampleProperty2")] Model model)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(RuleViewModel achievementrule)
        {
            AchievementRule existingRule = db.Rules.GetById((int)achievementrule.AchievementRuleId);

            if (existingRule != null && ModelState.IsValid)
            {                                   
                existingRule.Title = achievementrule.Title;
                existingRule.TicketsCount = achievementrule.TicketsCount;
                existingRule.TimespanDays = achievementrule.TimespanDays;
                existingRule.Priority = achievementrule.Priority;                
               
                var existingAch = db.Achievements.GetById((int)achievementrule.Achievement);
                existingRule.Achievement = existingAch;
                //db.Rules.Update(existingRule);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(existingRule);
        }

        // GET: /Administration/Rules/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AchievementRule achievementrule = db.Rules.GetById((int)id);
            if (achievementrule == null)
            {
                return HttpNotFound();
            }
            return View(achievementrule);
        }

        // POST: /Administration/Rules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AchievementRule achievementrule = db.Rules.GetById((int)id);
            db.Rules.Delete(achievementrule);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

        private RuleViewModel ConvertToViewModel(AchievementRule achievementrule)
        {
            RuleViewModel model = new RuleViewModel()
            {
                AchievementRuleId = achievementrule.AchievementRuleId,
                Title = achievementrule.Title,
                TicketsCount = achievementrule.TicketsCount,
                TimespanDays = achievementrule.TimespanDays,
                Priority = achievementrule.Priority                
            };
            model.Achievement = achievementrule.Achievement.AchievementId;
            return model;
        }

    }
}
