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
        public IRepositoryGeneric<Cliente> _client;
        private readonly SistemaFacturacionContext _context;

        public UnitOfWork( SistemaFacturacionContext context)
        {
          
            _context = context;
        }

        public IRepositoryGeneric<Cliente> Client
        {

            get { return _client == null? new RepositoryGeneric<Cliente>(_context): _client; }
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
