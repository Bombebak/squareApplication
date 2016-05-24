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
            ViewBag.Message = "About the Squares concept";
          
            return View();
        }

        public ActionResult Landing()
        {
            return View();
        }

     
    }
}