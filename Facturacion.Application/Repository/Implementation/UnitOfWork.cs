using Facturacion.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facturacion.Infrastruture.ApplicationDbContext;
using Facturacion.Application.Repository.Interfaces;

namespace Facturacion.Application.Repository.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        public IRepositoryGeneric<Client> client;
        public IRepositoryGeneric<Invoice> invoice;
        public IRepositoryGeneric<InvoiceDetail> invoiceDetails;
        public IRepositoryGeneric<Product> product;
        public IRepositoryGeneric<Category> category;
        public IRepositoryGeneric<Correlative> correlat;

        private readonly SistemaFacturacionContext _context;

        public UnitOfWork(SistemaFacturacionContext context)
        {

            _context = context;
        }

        public IRepositoryGeneric<Client> Client
        {

            get { return client == null ? new RepositoryGeneric<Client>(_context) : client; }
        }

        public IRepositoryGeneric<Invoice> Invoice
        {

            get { return invoice == null ? new RepositoryGeneric<Invoice>(_context) : invoice; }

        }



        public IRepositoryGeneric<InvoiceDetail> InvoiceDetails
        {
            get { return invoiceDetails == null ? new RepositoryGeneric<InvoiceDetail>(_context) : invoiceDetails; }

        }

        public IRepositoryGeneric<Product> Product
        {

            get { return product == null ? new RepositoryGeneric<Product>(_context) : product; }
        }

        public IRepositoryGeneric<Category> Category
        {

            get { return category == null ? new RepositoryGeneric<Category>(_context) : category; }
        }

        public IRepositoryGeneric<Correlative> Correlat
        {
            get { return correlat == null ? new RepositoryGeneric<Correlative>(_context) : correlat; }
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
