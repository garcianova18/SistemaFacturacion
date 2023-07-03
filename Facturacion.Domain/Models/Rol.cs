using System;
using System.Collections.Generic;

namespace Facturacion.Domain.Models
{
    public partial class Rol
    {
        public Rol()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string RolName { get; set; }
        public bool? Status { get; set; }
        public DateTime? DateCreate { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
