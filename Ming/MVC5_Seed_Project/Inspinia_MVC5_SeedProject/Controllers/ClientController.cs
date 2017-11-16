using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Inspinia_MVC5_SeedProject.Models; //using model

namespace Inspinia_MVC5_SeedProject.Controllers
{
    public class ClientController : Controller
    {
        // GET: Client
        public ActionResult Index()
        {
            using (OurDBContext db = new OurDBContext())
            {
                return View(db.clientAccount.ToList());
            }
            //return View();
        }

        // GET: Client/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Client/Create
        [HttpPost]
        public ActionResult Create(Clients client)
        {
            if (ModelState.IsValid)
            {
                using (OurDBContext db = new OurDBContext())
                {
                    db.clientAccount.Add(client);
                    db.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.Message = client.CustomerNumber + "client creation is done";
            }
            return View();
        }
    }
}