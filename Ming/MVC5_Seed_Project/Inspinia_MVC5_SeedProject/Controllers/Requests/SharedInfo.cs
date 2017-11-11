namespace Inspinia_MVC5_SeedProject.Controllers.Requests
{
    public class SharedInfo
    {
        public string ClientCode { get; set; }
        public string ProviderKey { get; set; }
        public string CustomerNumber { get; set; }
        public string RequisitionId { get; set; }
        
        // [RECEIPTID] for AssessmentStatus, the receipt of the initial AssessmentOrderRequest.
        public string ReceiptId { get; set; }     
        
    }
}