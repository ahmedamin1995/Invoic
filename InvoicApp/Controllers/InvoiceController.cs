using InvoicApp.Models;
using Invoice.Core.Domains;
using Invoice.Infrastructure.Repostory;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoicApp.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly IInoviceRepository _inoviceRepository;
        private readonly IInvoiceDetailRepository _invoiceDetailRepository;

        //invoice
        [BindProperty]
        public InvoiceViewModel invoiceViewModel { get; set; }
        public InvoiceController(IInoviceRepository inoviceRepository, IInvoiceDetailRepository invoiceDetailRepository)
        {
            this._inoviceRepository = inoviceRepository;
            this._invoiceDetailRepository = invoiceDetailRepository;
            invoiceViewModel = new InvoiceViewModel
            {
                Items = _inoviceRepository.GetAllITems(),
                Customers=_inoviceRepository.GetAllCustomers(),
                Stores= _inoviceRepository.GetAllStores(),
                Units= _inoviceRepository.GetAllUnits(),
                invoices= new Invoice.Core.Domains.Invoices(),
                InvoiceDetail=new Invoice.Core.Domains.InvoiceDetail()
            };
        }
        public IActionResult Index()
        {
            List<InvoiceIndexModel> InvoiceList = new List<InvoiceIndexModel>();

            var invoices = _inoviceRepository.GetAll();
            var invoicesDetails = _invoiceDetailRepository.GetAll();
            foreach (var item in invoices)
            {
                InvoiceList.Add(new InvoiceIndexModel
                {
                    invoices= item,
                    invoiceDetails= invoicesDetails.Where(x=>x.InvoicesID==item.ID)
                });
            }
          
            return View(InvoiceList);
        }

        public IActionResult Create()
        {
            return View(invoiceViewModel);
        }
        [HttpPost]
        public async Task<ActionResult> CreateInvoice([FromBody] InvoiceRequestModel invoiceRequestModel)
        {
            
            if(invoiceRequestModel !=null && invoiceRequestModel.invoiceDetail != null)
            {
                decimal total = 0;
                decimal net = 0;
                var newinvoice = new Invoices
                {
                    SerialNo = invoiceRequestModel.serialNo,
                    Createdate = Convert.ToDateTime(invoiceRequestModel.createdate),
                    CustomerID = Convert.ToInt32(invoiceRequestModel.customerID),
                    StoreID = Convert.ToInt32(invoiceRequestModel.storeID),
                };

                foreach (invoiceDetail item in invoiceRequestModel.invoiceDetail)
                {

                    total += (Convert.ToDecimal(item.price) * Convert.ToDecimal(item.quantity));
                    net += (Convert.ToDecimal(item.price) * Convert.ToDecimal(item.quantity)) - Convert.ToDecimal(item.discount);


                }
                newinvoice.Net = net;
                newinvoice.Total = total;
                _inoviceRepository.AddEntity(newinvoice);
                
              await  _inoviceRepository.Save();

                foreach (invoiceDetail item in invoiceRequestModel.invoiceDetail)
                {
                    var newinvoiceDetail = new InvoiceDetail
                    {
                        Discount = Convert.ToDecimal(item.discount),
                        Price = Convert.ToDecimal(item.price),
                        ItemID = Convert.ToInt32(item.itemID),
                        UnitID = Convert.ToInt32(item.unitID),
                        Quantity = Convert.ToDecimal(item.quantity),
                        InvoicesID = newinvoice.ID

                    };
                    _invoiceDetailRepository.Add(newinvoiceDetail);
                 await   _invoiceDetailRepository.Save();


                }



            }

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Edit(int ID)
        {
          var InvoiceViewModel=  new InvoiceViewModel
            {
                Items = _inoviceRepository.GetAllITems(),
                Customers = _inoviceRepository.GetAllCustomers(),
                Stores = _inoviceRepository.GetAllStores(),
                Units = _inoviceRepository.GetAllUnits(),
                invoices = _inoviceRepository.FirstOrDefault(filter:x=>x.ID==ID),
                InvoiceDetail = _invoiceDetailRepository.FirstOrDefault(filter: x => x.InvoicesID == ID)
          };
            InvoiceViewModel.invoices.InvoiceDetails = _invoiceDetailRepository.GetAll(filter: x => x.InvoicesID == ID).ToList();
            return View(InvoiceViewModel);
        }
        [HttpPost]

        public async Task<ActionResult> Edit([FromBody] InvoiceRequestModel invoiceRequestModel)
        {

            if (invoiceRequestModel != null && invoiceRequestModel.invoiceDetail != null)
            {
                _invoiceDetailRepository.RemoveRange(_invoiceDetailRepository.GetAll(filter: x => x.InvoicesID == Convert.ToInt32(invoiceRequestModel.invoicID)));
                await _invoiceDetailRepository.Save();
                decimal total = 0;
                decimal net = 0;

                var oldInvoice = _inoviceRepository.FirstOrDefault(filter: x => x.ID == Convert.ToInt32(invoiceRequestModel.invoicID));
                oldInvoice.SerialNo = invoiceRequestModel.serialNo;
                oldInvoice.Createdate = Convert.ToDateTime(invoiceRequestModel.createdate);
                oldInvoice.CustomerID = Convert.ToInt32(invoiceRequestModel.customerID);
                oldInvoice.StoreID = Convert.ToInt32(invoiceRequestModel.storeID);
                foreach (invoiceDetail item in invoiceRequestModel.invoiceDetail)
                {

                    total += (Convert.ToDecimal(item.price) * Convert.ToDecimal(item.quantity));
                    net += (Convert.ToDecimal(item.price) * Convert.ToDecimal(item.quantity)) - Convert.ToDecimal(item.discount);


                }
                oldInvoice.Net = net;
                oldInvoice.Total = total;
                await _inoviceRepository.Save();


                foreach (invoiceDetail item in invoiceRequestModel.invoiceDetail)
                {
                    var newinvoiceDetail = new InvoiceDetail
                    {
                        Discount = Convert.ToDecimal(item.discount),
                        Price = Convert.ToDecimal(item.price),
                        ItemID = Convert.ToInt32(item.itemID),
                        UnitID = Convert.ToInt32(item.unitID),
                        Quantity = Convert.ToDecimal(item.quantity),
                        InvoicesID = oldInvoice.ID

                    };
                    _invoiceDetailRepository.Add(newinvoiceDetail);
                    await _invoiceDetailRepository.Save();


                }
               
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<ActionResult> Delete (int ID)
        {
            _invoiceDetailRepository.RemoveRange(_invoiceDetailRepository.GetAll(filter: x => x.InvoicesID == ID));
          await  _invoiceDetailRepository.Save();

            var invoices = _inoviceRepository.GetAll(filter: x => x.ID == ID).FirstOrDefault();
                _inoviceRepository.Remove(invoices);

          await  _inoviceRepository.Save();
            return RedirectToAction(nameof(Index));
        }
    }
}
