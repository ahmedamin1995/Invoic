using Invoice.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoice.Infrastructure.Repostory
{
public interface IInoviceRepository : IRepository<Invoices>
    {

        List<Items> GetAllITems();

        List<Units> GetAllUnits();

        List<Stores> GetAllStores();


        List<Customers> GetAllCustomers();
        void AddEntity(Invoices entity);
     

    }
}
