using Facturacion.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facturacion.Infrastruture.ApplicationDbContext;

namespace Facturacion.Application.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private IRepositoryGeneric<Client> _client;
        private IRepositoryGeneric<Invoice> _invoice;
        private IRepositoryGeneric<InvoiceDetail> _invoiceDetails;
        private IRepositoryGeneric<Product> _product;
        private IRepositoryGeneric<Category> _category;
            
        private readonly SistemaFacturacionContext _context;

        public UnitOfWork( SistemaFacturacionContext context)
        {
          
            _context = context;
        }

        public IRepositoryGeneric<Client> Client
        {

            get { return _client == null? new RepositoryGeneric<Client>(_context): _client; }
        }

       public IRepositoryGeneric<Invoice> Invoice
        {
           
            get { return _invoice == null ? new RepositoryGeneric<Invoice>(_context) : _invoice; }
            
        }
        


       public  IRepositoryGeneric<InvoiceDetail> InvoiceDetails
       {
            get { return _invoiceDetails == null ? new RepositoryGeneric<InvoiceDetail>(_context) : _invoiceDetails; }

       }

       public IRepositoryGeneric<Product> Product
       {

                get { return _product == null ? new RepositoryGeneric<Product>(_context) : _product; }
       }

        public IRepositoryGeneric<Category> Category
        {

            get { return _category == null ? new RepositoryGeneric<Category>(_context) : _category; }
        }


        public async Task<int> Save()
        {
           return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
