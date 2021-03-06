﻿using System;
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
    public class QuestionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Questions
        public ActionResult Index()
        {
            var qustions = db.Questions.Include(u => u.Author);
            
            
            return View(qustions.OrderByDescending(d=>d.Date).ToList());
        }

        //Danger!!!!
        [Authorize]
        public ActionResult Like(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var question = db.Questions.FirstOrDefault(q => q.QuestionId == id);
            question.Ranking += 1;
            db.SaveChanges();
            return RedirectToAction("Details/"+id);
        }
        [Authorize]
        public ActionResult Dislike(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var question = db.Questions.FirstOrDefault(q => q.QuestionId == id);
            question.Ranking -= 1;
            db.SaveChanges();
            return RedirectToAction("Details/" + id);

        }
        // GET: Questions/Details/5
        public ActionResult Details(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Include(u => u.Author).FirstOrDefault(u => u.QuestionId == id);
            question.Tags = db.Tags.Where(t => t.QuestionId == id).ToList();
            question.Comments = db.Comments.Where(q => q.QuestionId == id).Include(q => q.Author).ToList();
            
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // GET: Questions/Creat
        [Authorize]
        
        public ActionResult Create()
        {
            return View();
        }

        // POST: Questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QuestionId,Title,Body,Date,Author,Tags,Ranking")] Question question)
        {
            
            if (ModelState.IsValid)
            {
                question.Author = db.Users.FirstOrDefault(a => a.UserName == User.Identity.Name);                
                db.Questions.Add(question);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(question);
        }

        // GET: Questions/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            Utiles.QuesionRating = question.Ranking;
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "QuestionId,Title,Body,Date")] Question question)
        {
            
            if (ModelState.IsValid)
            {
                question.Author = db.Users.FirstOrDefault(a => a.UserName == User.Identity.Name);
                db.Entry(question).State = EntityState.Modified;
                question.Ranking = Utiles.QuesionRating;
                db.SaveChanges();
                return RedirectToAction("Details/"+question.QuestionId);
            }
            return View(question);
        }

        // GET: Questions/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Questions/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Question question = db.Questions.Find(id);
            db.Questions.Remove(question);
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
