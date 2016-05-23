using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SquareApplication.Models;

namespace SquareApplication.Controllers
{
    public class CreateDesignController : Controller
    {
     
        public ActionResult CreateDesign(int set_id)
        {
            selectedSetViewModel selectedSet;
            using (SquaresEntities db = new SquaresEntities())
            {
                var set = db.Sets.Where(s => s.set_id == set_id).Include("Tiles").Include("User").FirstOrDefault();
                selectedSet = new selectedSetViewModel()
                {
                    name = set.name,
                    set_id = set.set_id,
                    upload_date = set.upload_date.Value.Date,
                    price = set.price.Value,
                    user_name = set.User.name,
            };
                foreach (var t in set.Tiles)
                {
                    selectedSet.tiles.Add(t.url);
                }
            }
            return View("CreateDesign", selectedSet);
        }
    }
}