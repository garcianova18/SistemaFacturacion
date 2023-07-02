using Facturacion.Application.Repository;
using Facturacion.Domain.Models;
using Facturacion.Infrastruture.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;

namespace Facturacion.Application.Services
{
    public interface IInvoiceServices
    {
        Task<Invoice> GetTotalSubtotalTax(Invoice invoice);
        string GetInvoiceNumber(long Number);
        Task<Invoice> GetInvoiceByNumber(string InvoiceNumber);
        Task<Invoice> GetInvoiceDetails(int id);
        Task<int> Checkstock(int Id, int Amount);
    }
    public class InvoiceServices: IInvoiceServices
    {
        private readonly SistemaFacturacionContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public InvoiceServices(SistemaFacturacionContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public String GetInvoiceNumber(long Number)
        {

            string NumberFcatura = "";

            if (Number >= 100000) 
            {
                NumberFcatura = Number.ToString();
                
            }
            else if (Number >= 10000)
            {

                NumberFcatura = string.Concat("0", Number.ToString());

            }
            else if (Number >= 1000) {

                NumberFcatura = string.Concat("00", Number.ToString());

            }
            else if (Number >= 100)
            {

                NumberFcatura = string.Concat("000", Number.ToString());

            }
            else if (Number >= 10)
            {

                NumberFcatura = string.Concat("0000", Number.ToString());

            }
            else
            {

                NumberFcatura = string.Concat("00000", Number.ToString());

            }
           

            return NumberFcatura;

        }

        public async Task<Invoice> GetTotalSubtotalTax(Invoice Invoice)
        {

            foreach (var details in Invoice.InvoiceDetails)
            {
                //get Product
                var getPorduct = await _unitOfWork.Product.GetByid(details.IdProduct);
            
                // add details 
                details.Price = getPorduct.Price;
                details.Total = details.Amount * getPorduct.Price;

                //actualizar el stock de cada producto
                var producto = await _unitOfWork.Product.GetByid(details.IdProduct);
                producto.Stock = producto.Stock - details.Amount;
                _unitOfWork.Product.Update(producto);
                //_unitOfWork.InvoiceDetails.Update(details);

            }

            // add Invoice
            Invoice.Ninvoice = GetInvoiceNumber(1);
            Invoice.Total = Invoice.InvoiceDetails.Sum(x => x.Total);
            Invoice.Itbis = Invoice.Total * 0.18M;
            Invoice.SubTotal = Invoice.Total - Invoice.Itbis;
            Invoice.DateCreate = DateTime.Now;

            return Invoice;
        }

        public async Task <Invoice> GetInvoiceByNumber(string InvoiceNumber)
        {

            return await _context.Invoices.FirstOrDefaultAsync(n=> n.Ninvoice == InvoiceNumber);

        }

        public async Task<Invoice> GetInvoiceDetails(int id)
        {

            return await _context.Invoices.Include(d=>d.InvoiceDetails).ThenInclude(p=>p.Product).Include(c=>c.Client).FirstOrDefaultAsync(n => n.Id == id);


        }

        public async Task<int> Checkstock(int Id, int Amount)
        {
            var Product = await _context.Products.FindAsync(Id);

            if (Product.Stock < Amount)
            {
                return 0;
            }

            return Product.Stock;
        }

    }
}
