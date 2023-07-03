using System;
using System.Collections.Generic;

namespace Facturacion.Domain.Models
{
    public partial class Correlative
    {
        public int Id { get; set; }
        public int LastNumber { get; set; }
        public DateTime DateCreate { get; set; }
    }
}
