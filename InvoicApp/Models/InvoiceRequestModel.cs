using Invoice.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoicApp.Models
{
    public class InvoiceRequestModel
    {
        public string invoicID { get; set; }
        public string serialNo { get; set; }
        public string storeID { get; set; }

        public string customerID { get; set; }
        public string createdate { get; set; }
        public invoiceDetail[] invoiceDetail { get; set; }

    }

    public class invoiceDetail
    {
        public string itemID { get; set; }
        public string unitID { get; set; }

        public string quantity { get; set; }

        public string price { get; set; }

        public string discount { get; set; }

    }
}
