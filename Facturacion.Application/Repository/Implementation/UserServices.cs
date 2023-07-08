using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facturacion.Application.Repository.Interfaces;
using Facturacion.Domain.DTOs;
using Facturacion.Domain.Models;
using Facturacion.Infrastruture.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;

namespace Facturacion.Application.Repository.Implementation
{
    public class UserServices: ILoginServices
    {
        private readonly SistemaFacturacionContext _context;

        public UserServices(SistemaFacturacionContext context)
        {
            _context = context;
        }
        public async Task<User> GetAdmin(User user)
        {

            return await _context.Users.Include(r=> r.Rol).FirstOrDefaultAsync(e=>e.Email == user.Email && e.Password ==user.Password);
        }

    }
}
