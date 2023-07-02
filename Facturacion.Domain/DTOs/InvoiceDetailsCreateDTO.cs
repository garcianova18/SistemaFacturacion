using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Facturacion.Domain.DTOs
{
    public class InvoiceDetailsCreateDTO
    {
        [Required()]
        [Range(1,double.MaxValue, ErrorMessage ="El valor de {0} debe ser mayor a 0")]
        public int IdProduct { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "El valor de {0} debe ser mayor a 0")]
        public int Amount { get; set; }
        public decimal Discount { get; set; }
    }
}
