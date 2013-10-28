using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TicketRewardSystem.Models;
using TicketRewardSystem.ViewModels;
using TicketRewardSystem.Repository;

namespace TicketRewardSystem.Areas.Administration.Controllers
{
    public class AchievementsController : AdminBaseController
    {
        private UowData db = new UowData();
        private const string DefaultImgUrl = "~/img/achievement-default.gif";
        private const string DefaultImgPathFolder = "~/img/";

        // GET: /Administration/Achievements/
        public ActionResult Index()
        {
            return View(db.Achievements.All().ToList());
        }

        public JsonResult GetAll()
        {
            var allAchievements = db.Achievements.All().Select(AchievementViewModel.FromAchievement);
            return Json(allAchievements, JsonRequestBehavior.AllowGet);
            //return null;
        }

        // GET: /Administration/Achievements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Achievement achievement = db.Achievements.GetById((int)id);
            if (achievement == null)
            {
                return HttpNotFound();
            }
            return View(achievement);
        }

        // GET: /Administration/Achievements/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Administration/Achievements/Create
		// To protect from over posting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		// 
		// Example: public ActionResult Update([Bind(Include="ExampleProperty1,ExampleProperty2")] Model model)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Achievement achievement)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(achievement.ImageUrl))
                {
                    achievement.ImageUrl = DefaultImgUrl;
                }
                db.Achievements.Add(achievement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(achievement);
        }

        // GET: /Administration/Achievements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Achievement achievement = db.Achievements.GetById((int)id);
            if (achievement == null)
            {
                return HttpNotFound();
            }
            return View(achievement);
        }

        // POST: /Administration/Achievements/Edit/5
		// To protect from over posting attacks, please enable the specific properties you want to bind to, for 
		// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
		// 
		// Example: public ActionResult Update([Bind(Include="ExampleProperty1,ExampleProperty2")] Model model)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Achievement achievement)
        {
            if (ModelState.IsValid)
            {
                db.Achievements.Update(achievement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(achievement);
        }

        // GET: /Administration/Achievements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Achievement achievement = db.Achievements.GetById((int)id);
            if (achievement == null)
            {
                return HttpNotFound();
            }
            return View(achievement);
        }

        // POST: /Administration/Achievements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Achievement achievement = db.Achievements.GetById((int)id);
            db.Achievements.Delete(achievement);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
