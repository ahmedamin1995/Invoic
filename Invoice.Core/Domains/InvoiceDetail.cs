using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Core.Domains
{
  public  class InvoiceDetail
    {
        public int Id { get; set; }

        public int InvoicesID { get; set; }
        [ForeignKey("InvoicesID")]
        public Invoices Invoice { get; set; }
        public int ItemID { get; set; }

        [ForeignKey("ItemID")]
        public Items Item { get; set; }


        public int UnitID { get; set; }

        [ForeignKey("UnitID")]
        public Units Unit { get; set; }

        public decimal Price { get; set; }
        public decimal Quantity { get; set; }
        public decimal Discount { get; set; }


    }
}
