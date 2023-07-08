using Facturacion.Application.Repository.Interfaces;
using Facturacion.Domain.DTOs;
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
            //para crear el numero de la factura de solo 6 digitos
            InvoiceNumber = InvoiceNumber.Substring(InvoiceNumber.Length - AmountDigit, AmountDigit);


            return InvoiceNumber;

        }

        public async Task<Invoice> GetTotalSubtotalTax(Invoice Invoice)
        {
            //========agregar o actualizar los detalles de la factura---------------//

            foreach (var details in Invoice.InvoiceDetails)
            {
                //obtener el precio de cada producto
                var product = await _unitOfWork.Product.GetByid(details.IdProduct);

                // calcular total de los detalles
                details.Price = product.Price;
                details.Total = details.Amount * product.Price - details.Discount;

                //actualizar el stock de cada producto
                product.Stock = product.Stock - details.Amount;
                _unitOfWork.Product.Update(product);

                //si entra aqui vamos a actualizar los detalles de la factura
                if (Invoice.Id !=0)
                {
                    _unitOfWork.InvoiceDetails.Update(details);
                }

              

            }
           
            //==================Correlativo Crear numero de fasctura ==============//

            //Si entra aqui estamos creando una factura de lo contrario estamos actualizando
            if (Invoice.Ninvoice == string.Empty || Invoice.Ninvoice is null)
            {
                //obetener el ultimo numero de factura creado 
                var GetCorrelative = await _unitOfWork.Correlat.GetByid(1);

                GetCorrelative.LastNumber++;
                GetCorrelative.DateCreate = DateTime.Now;
                _unitOfWork.Correlat.Update(GetCorrelative);

                //agregar el numero de factura a la factura
                Invoice.Ninvoice = CreateInvoiceNumber(GetCorrelative.LastNumber);
            }

            //===========agregar o actualizar la cabecera de la factura===================//
            
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

        public async Task<bool> Checkstock(int Id, int Amount)
        {
            var Product = await _context.Products.FindAsync(Id);

            if (Product.Stock < Amount)
            {
                return false;
            }

            return true;
        }

        public async Task< ExistsProducDTO >ExistsProduct( Invoice invoice)
        {

            int idnotvalid = 0;
            bool existsProduct = false;
            
            foreach (var item in invoice.InvoiceDetails)
            {

                existsProduct = await _unitOfWork.Product.Exists(d => d.Id == item.IdProduct);

                idnotvalid = item.IdProduct;
                if (existsProduct is false)
                {
                    break;
                }
            }

            return new ExistsProducDTO { Id= idnotvalid , IsSuccess = existsProduct};
        }

        public async Task<ExistsProducDTO> ExistsDetails(Invoice invoice)
        {

            int idnotvalid = 0;
            bool existsdetails = false;

            foreach (var item in invoice.InvoiceDetails)
            {

                existsdetails = await _unitOfWork.InvoiceDetails.Exists(d => d.Id == item.Id);

                idnotvalid = item.Id;
                if (existsdetails is false)
                {
                    break;
                }
            }

            return new ExistsProducDTO { Id = idnotvalid, IsSuccess = existsdetails };
        }

        public async Task<Invoice> GetInvoiceAsNotraking(int id)
        {

            return await _context.Invoices.AsNoTracking().FirstOrDefaultAsync(x=> x.Id == id);

        }



    }
}
