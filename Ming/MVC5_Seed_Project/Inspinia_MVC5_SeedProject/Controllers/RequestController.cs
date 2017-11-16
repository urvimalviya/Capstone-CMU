using System.IO;
using System.Net;
using System.Web.Mvc;
using Inspinia_MVC5_SeedProject.Controllers.Requests;
using SuperXML;

namespace Inspinia_MVC5_SeedProject.Controllers
{
    public class RequestController : Controller
    {
        [HttpPost]
        public void AssessmentOrderRequest()
        {
            var info = new SharedInfo
            {
                ClientCode = "001",
                ProviderKey = "abcdef",
                CustomerNumber = "Coke",
                RequisitionId = "test01"    //Assessment id
            };

            var order = new AssessmentOrder
            {
                EmployeeNumber = "1",
                CallBackUri = "http://localhost:5001/",
                Requestor = "Coke",
                LastName = "Cao",
                FirstName = "Tianyi",
                CandidateEmail = "tcao@andrew.cmu.edu"
            };

            var xml = GenerateAssessmentOrderRequestXml(info, order);
            PostXmlData("http://localhost:5001/SelectServer/ReceiveXmlData", xml);
        }

        [HttpPost]
        public void AssessmentStatusRequest()
        {
            var info = new SharedInfo
            {
                ClientCode = "001",
                ProviderKey = "abcdef",
                CustomerNumber = "00001",
                RequisitionId = "test01"
            };
            
            
        }
        
        
        
        
        public string GenerateAssessmentOrderRequestXml(SharedInfo info, AssessmentOrder order)
        {
            var compiler = new Compiler()
                .AddKey("ClientCode", info.ClientCode)
                .AddKey("ProviderKey", info.ProviderKey)
                .AddKey("CustomerNumber", info.CustomerNumber)
                .AddKey("RequisitionId", info.RequisitionId)
                .AddKey("EmployeeNumber", order.EmployeeNumber) // same to UniqueIdentifier
                .AddKey("CallBackUri", order.CallBackUri)
                .AddKey("Requestor", order.Requestor)
                .AddKey("LastName", order.LastName)
                .AddKey("FirstName", order.FirstName)
                .AddKey("CandidateEmail", order.CandidateEmail);

            var path = Directory.GetCurrentDirectory() + "/Controllers/Requests/AssessmentOrderTemplate.xml";
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