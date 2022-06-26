
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Invoice.Core.Domains;

namespace Invoice.Infrastructure.Data
{
  public  class InvoiceContextSeed
    {
        public static async Task SeedAsync(InvoiceContext context, ILoggerFactory loggerFactory)
        {
			try
			{
                //var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
             
                if (!context.Items.Any())
				{
                    var ItemsData =
                       File.ReadAllText("../Invoice.Infrastructure/Data/SeedData/Items.json");

                    var Items = JsonSerializer.Deserialize<List<Items>>(ItemsData);


                    
                        context.Items.AddRange(Items);
                    



                    await context.SaveChangesAsync();

                }


                if (!context.Units.Any())
                {
                    var UnitsData =
                       File.ReadAllText("../Invoice.Infrastructure/Data/SeedData/Units.json");

                    var Units = JsonSerializer.Deserialize<List<Units>>(UnitsData);



                    context.Units.AddRange(Units);




                    await context.SaveChangesAsync();

                }
                if (!context.Customers.Any())
                {
                    var CustomersData =
                        File.ReadAllText("../Invoice.Infrastructure/Data/SeedData/Customers.json");

                    var Customers = JsonSerializer.Deserialize<List<Customers>>(CustomersData);


                   
                        context.Customers.AddRange(Customers);
                    

                    await context.SaveChangesAsync();
                }

                if (!context.Stores.Any())
                {
                    var StoresData =
                        File.ReadAllText("../Invoice.Infrastructure/Data/SeedData/Stores.json");

                    var stores = JsonSerializer.Deserialize<List<Stores>>(StoresData);


                 
                        context.Stores.AddRange(stores);
                   


                    await context.SaveChangesAsync();
                }

            }
			catch (Exception ex)
			{

                var logger = loggerFactory.CreateLogger<InvoiceContextSeed>();
                logger.LogError(ex.Message);
            }
        }
    }
}
