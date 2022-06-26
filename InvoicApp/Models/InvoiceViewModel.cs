using Invoice.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoicApp.Models
{
    public class InvoiceViewModel
    {
        public Invoices invoices { get; set; }

        public InvoiceDetail InvoiceDetail { get; set; }

        public IEnumerable<Stores> Stores { get; set; }
        public IEnumerable<Customers> Customers { get; set; }

        public IEnumerable<Units> Units { get; set; }
        public IEnumerable<Items> Items { get; set; }




    }
}
