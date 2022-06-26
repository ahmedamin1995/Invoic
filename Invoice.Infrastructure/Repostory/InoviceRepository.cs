using Invoice.Core.Domains;
using Invoice.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Infrastructure.Repostory
{
    public class InoviceRepository: Repository<Invoices>, IInoviceRepository
    {
        private readonly InvoiceContext _db;
        public InoviceRepository(InvoiceContext db):base(db)
        {
            _db = db;
        }



       // this Function To Bind DropdownList
        public List<Items> GetAllITems()
        {
            return _db.Items.ToList();
        }
        public List<Units> GetAllUnits()
        {
            return _db.Units.ToList();
        }
        public List<Stores> GetAllStores()
        {
            return _db.Stores.ToList();
        }
        public List<Customers> GetAllCustomers()
        {
            return _db.Customers.ToList();
        }

        public void AddEntity(Invoices entity)
        {
            _db.Invoices.Add(entity);
        }
    }
}
