using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Invoice.Core.Domains;
namespace Invoice.Infrastructure.Data
{
   public class InvoiceContext:DbContext
    {
        public InvoiceContext(DbContextOptions<InvoiceContext> options) : base(options)
        {

        }
        public DbSet<Units> Units { get; set; }

        public DbSet<Stores> Stores { get; set; }

        public DbSet<Items> Items { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Invoices> Invoices { get; set; }

        public DbSet<InvoiceDetail> InvoiceDetails { get; set; }



    }
}
