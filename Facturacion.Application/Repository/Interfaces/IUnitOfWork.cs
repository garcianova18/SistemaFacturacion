using Facturacion.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion.Application.Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IRepositoryGeneric<Client> Client { get; }
        public IRepositoryGeneric<Invoice> Invoice { get; }
        public IRepositoryGeneric<InvoiceDetail> InvoiceDetails { get; }
        public IRepositoryGeneric<Product> Product { get; }
        public IRepositoryGeneric<Category> Category { get; }
        public IRepositoryGeneric<Correlative> Correlat { get; }


        Task<int> Save();




    }
}
