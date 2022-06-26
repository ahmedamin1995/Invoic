using Invoice.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoicApp.Models
{
    public class InvoiceIndexModel
    {
        public Invoices invoices { get; set; }

        public IEnumerable<InvoiceDetail> invoiceDetails { get; set; }
    }
}
