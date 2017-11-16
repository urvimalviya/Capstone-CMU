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

                //sending request to external vendor and other XML part handled here
                //if this is successful, need to make changes in UploadFileController too
                //should we do this async?
                using (OurDBContext db = new OurDBContext())
                {
                    Candidates cand1 = db.candidateAccount.Single(g => g.UserName == candidate.UserName);
                    string clientCode = cand1.Requestor;
                    Client client = db.clientAccount.Find(clientCode);

                    RequestController rc = new RequestController();
                    rc.AssessmentOrderRequest(clientCode, client.ProviderKey , client.CustomerNumber,
                        "test01", candidate.UserID, client.CallBackUri,
                        candidate.LastName, candidate.FirstName, candidate.Email);
                }
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