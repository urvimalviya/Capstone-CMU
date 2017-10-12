using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inspinia_MVC5_SeedProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["SubTitle"] = "Main page for super admin";
            ViewData["Message"] = "Click the different page for next step";

            return View();
        }

        public ActionResult Registration()
        {
            ViewData["SubTitle"] = "Register candidates' infro";
            ViewData["Message"] = "Do it in batch or manually";

            return View();
        }

        public ActionResult TalentList()
        {
            ViewData["SubTitle"] = "List of all candiates";
            ViewData["Message"] = "Dashboard for candiate list";

            return View();
        }

        public ActionResult FileUpload()
        {
            ViewData["SubTitle"] = "XXX";
            ViewData["Message"] = "OOO";

            return View();
        }
    }
}