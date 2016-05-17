using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SquareApplication.Controllers
{
    public class CreateDesignController : Controller
    {
        // GET: CreateDesign
        public ActionResult Index()
        {
            return View("CreateDesign");
        }
    }
}