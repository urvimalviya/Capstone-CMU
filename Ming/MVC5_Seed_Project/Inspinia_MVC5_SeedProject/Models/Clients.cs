using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Inspinia_MVC5_SeedProject.Models
{
    public class Clients
    {
        [Key]
        public string ClientCode { get; set; }
        public string ProviderKey { get; set; }
        public string CustomerNumber { get; set; } //Requestor
        public string CallBackUri { get; set; }
    }
}