
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion.Domain.DTOs
{
    public class InvoiceWhitDetailsDTO
    {

        public int Id { get; set; }


        public string NInvoice { get; set; } = null!;
        public DateTime DateCreate { get; set; }

        //propiedad compuesta para realizar mappeo con Automapper
        public string ClientFirstName { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Itbis { get; set; }
        public decimal Total { get; set; }
        public virtual ICollection<invoiceDetailsDTO> InvoiceDetails { get; set; }

        
        
    }

}
