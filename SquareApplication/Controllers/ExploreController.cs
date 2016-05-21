using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SquareApplication.Models;

namespace SquareApplication.Controllers
{
    public class ExploreController : Controller
    {
        // GET: Explore
        public ActionResult Explore()
        {
            IList<Set> sets = new List<Set>();
                     
            using (SquaresEntities db = new SquaresEntities())
            {
                sets = db.Sets.Include("Tiles").Include("User").ToList();
            }
            ViewBag.sets = sets;
            return View();
        }

        [HttpPost]
        public ActionResult Explore(int set_id)
        {
            
            return RedirectToAction("CreateDesign", "CreateDesign", new {set_id = set_id});
        }
    }
}