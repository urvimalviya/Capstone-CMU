using System.Linq;
using System.Web.Mvc;
using Inspinia_MVC5_SeedProject.Models; //using model
using Inspinia_MVC5_SeedProject.Helpers;

namespace Inspinia_MVC5_SeedProject.Controllers
{
    public class CandidateController : Controller
    {
        // GET: Candidate
        public ActionResult Index()
        {
            using (OurDBContext db = new OurDBContext())
            {
                return View(db.candidateAccount.ToList());
            }
            //return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Candidates candidate)
        {
            if (ModelState.IsValid)
            {
                using (OurDBContext db = new OurDBContext())
                {
                    db.candidateAccount.Add(candidate);
                    db.SaveChanges();
                }

                //send a registration email once changes are saved in the database
                CustomEmail regMail = new CustomEmail();
                regMail.sendMail(candidate.Email, "Select International Registration", "Welcome to our platform!");

                ModelState.Clear();
                ViewBag.Message = candidate.FirstName + "" + candidate.LastName + "registration done";

            }
            return View();
        }

        //Login
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Login(Candidates can)
        {
            using (OurDBContext db = new OurDBContext())
            {
                //var usr = db.userAccount.Where(u => u.Username == user.Username && u.Password == user.Password).FirstOrDefault();
                var usr = db.candidateAccount.Where(u => u.UserName == can.UserName && u.Password == can.Password).FirstOrDefault();
                if (usr != null)
                {
                    Session["UserId"] = can.UserID.ToString();
                    Session["UserName"] = can.UserName.ToString();
                    return RedirectToAction("LoggedIn");
                }
                else {
                    ModelState.AddModelError("", "loging info wrong");
                }
            }

            return View();
        }

        public ActionResult LoggedIn()
        {
            if (Session["UserId"] != null)
            {
                return View();
            } else
            {
                return RedirectToAction("Login");
            }
        }
    }
}