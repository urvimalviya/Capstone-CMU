using Inspinia_MVC5_SeedProject.Models;
using System;
using System.Web.Mvc;

namespace Inspinia_MVC5_SeedProject.Controllers
{

    public class UploadFileController : Controller
    {
        [HttpPost]
        public ActionResult UploadFile()
        {
            //open file
            if (Request.Files.Count == 1)
            {
                //get file
                var postedFile = Request.Files[0];
                if (postedFile.ContentLength > 0)
                {
                    //read data from input stream
                    using (var csvReader = new System.IO.StreamReader(postedFile.InputStream))
                    {
                        string inputLine = "";
                        
                        // Skip the firts line
                        csvReader.ReadLine();
                        
                        //read each line
                        while ((inputLine = csvReader.ReadLine()) != null)
                        {
                            //get lines values
                            string[] values = inputLine.Split(new char[] { ',','\t' });

                            Candidates candidate = new Candidates
                            {
                                FirstName = values[0],
                                LastName = values[1],
                                UserName = values[2],
                                Email = values[3],
                                Password = values[4],
                                ProjectId = values[5]
                            };

                            if (ModelState.IsValid)
                            {
                                using (OurDBContext db = new OurDBContext())
                                {
                                    db.candidateAccount.Add(candidate);
                                    db.SaveChanges();
                                }
                                ModelState.Clear();
                                ViewBag.Message = candidate.FirstName + "" + candidate.LastName + "gegistartion done";
                            }

                        }
 
                        csvReader.Close();
                    }
                }
            }
 
            return Redirect("/Home/Participant");            
        }
    }
    
    
}