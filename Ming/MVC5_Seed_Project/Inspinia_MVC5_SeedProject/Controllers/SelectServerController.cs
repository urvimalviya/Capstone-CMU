using System;
using System.IO;
using System.Net;
using System.Web.Mvc;
using System.Xml.Linq;

namespace Inspinia_MVC5_SeedProject.Controllers
{
    public class SelectServerController : Controller
    {
        [HttpPost]
        public void ReceiveXmlData()
        {
            Console.Write("Received\n");
            
            
            
            string strmContents="";

            using(System.IO.StreamReader reader = new System.IO.StreamReader(Request.InputStream))
            {
                while (reader.Peek() >= 0)
                {    
                    strmContents += reader.ReadLine();
                }
            }
            
            Console.Write(strmContents);



        }
        
        
        
        
        public string PostXmlData(string destinationUrl, string requestXml)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(destinationUrl);
            byte[] bytes;
            bytes = System.Text.Encoding.ASCII.GetBytes(requestXml);
            request.ContentType = "text/xml; encoding='utf-8'";
            request.ContentLength = bytes.Length;
            request.Method = "POST";
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();
            var response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream responseStream = response.GetResponseStream();
                string responseStr = new StreamReader(responseStream).ReadToEnd();
                return responseStr;
            }
            return null;
        }
        
        
        
    }
    
}