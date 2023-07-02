using System;
using System.Collections.Generic;

namespace Facturacion.Domain.Models
{
    public partial class Client
    {
        public Client()
        {
            Invoices = new HashSet<Invoice>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Direccion { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Dni { get; set; }

        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
