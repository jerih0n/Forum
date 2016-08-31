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
    public class CommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: Comments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }
        [Authorize]
        public ActionResult Like(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var comment = db.Comments.FirstOrDefault(c => c.CommnetId == id);
            comment.Rating += 1;
            db.SaveChanges();
            return RedirectToAction("Details","Questions",new { id = comment.QuestionId });
        }
        [Authorize]
        public ActionResult Dislike(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var comment = db.Comments.FirstOrDefault(c => c.CommnetId == id);
            comment.Rating -= 1;
            db.SaveChanges();
            return RedirectToAction("Details", "Questions", new { id = comment.QuestionId });

        }
        // GET: Comments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CommnetId,CommentText,Date,Qustion,Date,QuestionId")] Comment comment)
        {
            var qustion = db.Questions.FirstOrDefault(q => q.QuestionId == Utiles.QustionId);
            if(User.Identity.Name != null)
            {
                qustion.Author = db.Users.FirstOrDefault(a => a.UserName == User.Identity.Name);
            }
            qustion.Comments.Add(comment);
            comment.Qustion = qustion;
            comment.QuestionId = Utiles.QustionId;
            if (ModelState.IsValid)
            {
                if(User.Identity.Name != null)
                {
                    comment.Author = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
                }
                                        
                db.Comments.Add(comment);                
                db.SaveChanges();
                //Possible Error
                return RedirectToAction("Details", "Questions",new { id =Utiles.QustionId });
            }
            return View(comment);
        }

        // GET: Comments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            Classes.Utiles.CommnetRating = comment.Rating;
            //Can harm!
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CommnetId,CommentText,Date,QuestionId")] Comment comment)
        {
            comment.QuestionId = ForumSystem.Classes.Utiles.QustionId;
            if (ModelState.IsValid)
            {
                comment.Rating = Utiles.CommnetRating;
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                
            }
            return RedirectToAction("Details", "Questions", new { id = Utiles.QustionId });
        }

        // GET: Comments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }
            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);
            db.Comments.Remove(comment);
            db.SaveChanges();
            return RedirectToAction("Details", "Questions", new { id = Utiles.QustionId });
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
