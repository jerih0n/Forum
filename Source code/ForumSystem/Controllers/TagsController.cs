using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ForumSystem.Classes;
namespace ForumSystem.Models
{
    public class TagsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Tags
        public ActionResult Index(int id )
        {
            //PERFORMANCE PROBLEM ?
            List<Question> sameTagedQuestions = new List<Question>();
            
            Tag tag = db.Tags.FirstOrDefault(t => t.TagId == id);
            Utiles.Tag = tag.TagText.ToString();
            List<Question> questions = db.Questions.Include(t => t.Tags).Include(u => u.Author).ToList();
            foreach (var question in questions) 
            {
                foreach(var questionTag in question.Tags)
                {
                    if (questionTag.TagText.ToLower() == tag.TagText.ToLower())
                    {
                        sameTagedQuestions.Add(question);
                    }
                }
            }
            
            return View(sameTagedQuestions.OrderByDescending(r=>r.Ranking).ToList());
        }


        // GET: Tags/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tag tag = db.Tags.Find(id);
            if (tag == null)
            {
                return HttpNotFound();
            }
            return View(tag);
        }

        // GET: Tags/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tags/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TagId,TagText,QuestionId")] Tag tag)
        {
            var question = db.Questions.FirstOrDefault(q => q.QuestionId == Utiles.QustionId);
            tag.Questions.Add(db.Questions.FirstOrDefault(q => q.QuestionId == Utiles.QustionId));
            tag.QuestionId = Utiles.QustionId;
            question.Tags.Add(tag);    
            if (ModelState.IsValid)
            {
                db.Tags.Add(tag);
                db.SaveChanges();
                           
                return RedirectToAction("Details","Questions",new { id = Utiles.QustionId});
            }

            return View(tag);
        }

        // GET: Tags/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tag tag = db.Tags.Find(id);
            if (tag == null)
            {
                return HttpNotFound();
            }
            return View(tag);
        }

        // POST: Tags/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TagId,TagText,QuestionId")] Tag tag)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tag).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tag);
        }

        // GET: Tags/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tag tag = db.Tags.Find(id);
            if (tag == null)
            {
                return HttpNotFound();
            }
            return View(tag);
        }

        // POST: Tags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tag tag = db.Tags.Find(id);
            db.Tags.Remove(tag);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
