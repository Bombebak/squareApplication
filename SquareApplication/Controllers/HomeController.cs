using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SquareApplication.Models;

namespace SquareApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("Landing");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            //using (SquaresEntities dbContext = new SquaresEntities())
            //{
            //    Console.WriteLine(dbContext.Database.Exists());
            //    ViewBag.Message = dbContext.Orders.FirstOrDefault().purchase_date.ToString();
            //}
            return View();
        }

        public ActionResult Landing()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}