using Invoice.Core.Domains;
using Invoice.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Infrastructure.Repostory
{
   public class InvoiceDetailRepostory : Repository<InvoiceDetail>, IInvoiceDetailRepository
    {
        private readonly InvoiceContext _db;
        public InvoiceDetailRepostory(InvoiceContext db) : base(db)
        {
            _db = db;
        }
    }
}
