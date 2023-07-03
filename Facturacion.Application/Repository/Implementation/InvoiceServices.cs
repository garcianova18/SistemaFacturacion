using Facturacion.Application.Repository.Interfaces;
using Facturacion.Domain.Models;
using Facturacion.Infrastruture.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace Facturacion.Application.Repository.Implementation
{
    
    public class InvoiceServices : IInvoiceServices
    {
        private readonly SistemaFacturacionContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public InvoiceServices(SistemaFacturacionContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public string CreateInvoiceNumber(int LastInvoiceNumber)
        {

            int AmountDigit = 6;

            //Cantidad de digitos a repetir
            string Digit = string.Concat(Enumerable.Repeat("0", AmountDigit));

            //cancatenamos los digitos con el ultimo numero correlativo
            string InvoiceNumber = Digit + LastInvoiceNumber.ToString();

            //indicamos la cantidad de digitos a eliminar a la izqierda
            //para mostrar para el numero de la factura de solo 6 digitos
            InvoiceNumber = InvoiceNumber.Substring(InvoiceNumber.Length - AmountDigit, AmountDigit);


            return InvoiceNumber;

        }

        public async Task<Invoice> GetTotalSubtotalTax(Invoice Invoice)
        {
            //----------agregar datos a los detalles de la factura---------------//
            foreach (var details in Invoice.InvoiceDetails)
            {
                //obtener el precio de cada producto
                var getPorduct = await _unitOfWork.Product.GetByid(details.IdProduct);

                // calcular total de los detalles
                details.Price = getPorduct.Price;
                details.Total = details.Amount * getPorduct.Price - details.Discount;

                //actualizar el stock de cada producto
                var producto = await _unitOfWork.Product.GetByid(details.IdProduct);
                producto.Stock = producto.Stock - details.Amount;
                _unitOfWork.Product.Update(producto);


            }

            //obetener el correlativo para crear numero de la facura
            var GetCorrelative = await _unitOfWork.Correlat.GetByid(1);

            GetCorrelative.LastNumber++;
            GetCorrelative.DateCreate = DateTime.Now;
            _unitOfWork.Correlat.Update(GetCorrelative);

            //--------agregra datos a la factura----------------//

            //Si Ninvoice no trae un numero se esta creando una factura de lo contrario se esta actuaizando
            if (Invoice.Ninvoice == string.Empty || Invoice.Ninvoice is null)
            {
                //crear numero de factura
                Invoice.Ninvoice = CreateInvoiceNumber(GetCorrelative.LastNumber);
            }
            Invoice.Total = Invoice.InvoiceDetails.Sum(x => x.Total);
            Invoice.Itbis = Invoice.Total * 0.18M;
            Invoice.SubTotal = Invoice.Total - Invoice.Itbis;
            Invoice.DateCreate = DateTime.Now;

            return Invoice;
        }

        public async Task<Invoice> GetInvoiceByNumber(string InvoiceNumber)
        {

            return await _context.Invoices.FirstOrDefaultAsync(n => n.Ninvoice == InvoiceNumber);

        }

        public async Task<Invoice> GetInvoiceDetails(int id)
        {

            return await _context.Invoices.Include(d => d.InvoiceDetails).ThenInclude(p => p.Product).Include(c => c.Client).FirstOrDefaultAsync(n => n.Id == id);


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
