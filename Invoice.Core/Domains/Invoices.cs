using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Core.Domains
{
   public class Invoices
    {
        public int ID { get; set; }

        public string SerialNo { get; set; }
        public DateTime Createdate { get; set; }

        public int CustomerID { get; set; }

        [ForeignKey("CustomerID")]
        public Customers Customer { get; set; }
        public int StoreID { get; set; }

        [ForeignKey("StoreID")]
        public Stores Store { get; set; }
        public decimal Total { get; set; }

        public decimal Net { get; set; }

        public ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
