using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ForumSystem.Models;
namespace ForumSystem.Controllers
{
    public class HomeController : Controller
    {   
        public ActionResult Index()
        {
            var db = new ApplicationDbContext();

            var questions = db.Questions.Select(u => u).ToList(); //TODO: Ordering by popularity!
            
            return View(questions);
        }
        
    }
}