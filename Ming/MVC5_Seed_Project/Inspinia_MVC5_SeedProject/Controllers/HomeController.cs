using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System;
using Microsoft.VisualBasic.FileIO;

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
        
        public ActionResult Participant()
        {
            ViewData["SubTitle"] = "Select International Online Test Platform";
            ViewData["Message"] = "Welcome Adam!";

            return View();
        }
        
        public void FileParser()
        {
            var path = @"/Users/terrycao/DownloadsQ/user.csv"; 
            
            using (TextFieldParser csvParser = new TextFieldParser(path))
            {
                csvParser.CommentTokens = new string[] { "#" };
                csvParser.SetDelimiters(new string[] { "," });
                csvParser.HasFieldsEnclosedInQuotes = true;

                // Skip the row with the column names
                csvParser.ReadLine();

                while (!csvParser.EndOfData)
                {
                    // Read current line fields, pointer moves to the next line.
                    string[] fields = csvParser.ReadFields();
                    string Name = fields[0];
                    string Address = fields[1];
                    Console.Write(Name + Address);
                }
            }
        }
    }
}