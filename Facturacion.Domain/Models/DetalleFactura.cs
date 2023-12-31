﻿using System;
using System.Collections.Generic;

namespace Facturacion.Domain.Models
{
    public partial class DetalleFactura
    {
        public int Id { get; set; }
        public int IdInvoice { get; set; }
        public int IdProduct { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public decimal Discount { get; set; }

        public virtual Invoice Invoice { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
