using Facturacion.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion.Application.Repository.Interfaces
{
    public interface ILoginServices
    {
        Task<User> GetAdmin(User user);
    }
}
