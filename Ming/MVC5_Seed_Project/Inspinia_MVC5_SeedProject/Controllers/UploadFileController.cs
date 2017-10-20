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
                            string[] values = inputLine.Split(new char[] { ',' });

                            String fname = values[0];
                            String lname = values[1];
                            String email = values[2];
                            String phone = values[3];
                            
                            

                        }
 
                        csvReader.Close();
                    }
                }
            }
 
            return Redirect("/home/uploadfilepage");            
        }
    }
    
    
}