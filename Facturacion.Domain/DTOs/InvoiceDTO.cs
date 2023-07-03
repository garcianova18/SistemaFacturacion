using Facturacion.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion.Domain.DTOs
{ 
    public class InvoiceDTO
    {
        public int Id { get; set; }
        public string NInvoice { get; set; } = null!;
        public DateTime DateCreate { get; set; }
        public int IdClient { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Itbis { get; set; }
        public decimal Total { get; set; }

      
    }
}

