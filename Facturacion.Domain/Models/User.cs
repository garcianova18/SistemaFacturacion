using System;
using System.Collections.Generic;

namespace Facturacion.Domain.Models
{
    public partial class User
    {
        public User()
        {
            Invoices = new HashSet<Invoice>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int? IdRol { get; set; }
        public string Password { get; set; }
        public bool? Status { get; set; }
        public virtual Rol Rol { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
