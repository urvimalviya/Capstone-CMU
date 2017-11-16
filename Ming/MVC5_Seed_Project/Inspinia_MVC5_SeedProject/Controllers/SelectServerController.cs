using System;
using System.IO;
using System.Net;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
using Inspinia_MVC5_SeedProject.Controllers.Requests;
using SuperXML;

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
            
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(strmContents);
            XmlElement root = doc.DocumentElement;
            String requestType = root.Name;
            Console.Write(requestType);
            string clientCode = "";
            string providerKey = "";
            string CustomerNumber = "";
            XmlNodeList clientId = doc.GetElementsByTagName("ClientId");
            if(clientId.Count >= 1)
            {
                // The tag could be found!
                clientCode= clientId[0].InnerText;
                Console.Write(clientCode);
            }
            
            XmlNodeList providerId = doc.GetElementsByTagName("ProviderId");
            if(providerId.Count >= 1)
            {
                // The tag could be found!
                providerKey= providerId[0].InnerText;
                Console.Write(providerKey);
            }
            
            XmlNodeList CustomerId = doc.GetElementsByTagName("ClientOrderId");
            if(CustomerId.Count >= 1)
            {
                // The tag could be found!
                CustomerNumber = CustomerId[0].InnerText;
                Console.Write(CustomerNumber);
            }
            if (requestType.Equals("AssessmentOrderRequest"))
            {
                SendAcknowledgementResponse(clientCode, providerKey, CustomerNumber);
            }
        }
        
        
        
        public void SendAcknowledgementResponse(string ClientCode, string ProviderKey, string CustomerNumber)
        {
            var info = new SharedInfo
            {
                ClientCode = "001",
                ProviderKey = "abcdef",
                CustomerNumber = "Coke",
                ReceiptId = "receipt0001"
            };

            var acknowledgement = new Acknowledgement()
            {
                AssessmentUrl = "http://assessment_url/",
                Description =  "Assessment descriptions"
                // Not sending status if not started
            };
            
            var xml = GenerateAcknowledgementResponseXml(info, acknowledgement);
            PostXmlData("http://localhost:5001/Request/GetAcknowledgementResponse", xml);
        }

        public string GenerateAcknowledgementResponseXml(SharedInfo info, Acknowledgement acknowledgement)
        {
            var compiler = new Compiler()
                .AddKey("ClientCode", info.ClientCode)
                .AddKey("ProviderKey", info.ProviderKey)
                .AddKey("ReceiptId", info.ReceiptId)
                .AddKey("CustomerNumber", info.CustomerNumber)
                .AddKey("AssessmentUrl", acknowledgement.AssessmentUrl)
                .AddKey("Description", acknowledgement.Description)
                .AddKey("Status", acknowledgement.Status)
                .AddKey("StatusDate", acknowledgement.StatusDate);
            
            var path = Directory.GetCurrentDirectory() + "/Controllers/Requests/AcknowledgementTemplate.xml";
            var result = compiler.CompileXml(path);

            return result;

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