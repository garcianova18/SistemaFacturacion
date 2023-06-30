using Facturacion.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion.Application.Repository
{
    public interface IUnitOfWork: IDisposable
    {
       public IRepositoryGeneric<Cliente> Client { get; }

        Task<int> Save();
        



    }
}
