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
     
        public ActionResult CreateDesign(int? set_id)
        {
            if (set_id == null)
            {
                // It means the request comes from the landing page
                // So we load the default set:
                set_id = 1;
            }
            selectedSetViewModel selectedSet;
            using (SquareDbEntities db = new SquareDbEntities())
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