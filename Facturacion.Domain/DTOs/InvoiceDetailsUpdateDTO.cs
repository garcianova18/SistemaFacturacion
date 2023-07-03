using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion.Domain.DTOs
{
    public class InvoiceDetailsUpdateDTO
    {
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "El valor de {0} debe ser mayor a 0")]
        public int Id { get; set; }

        [Required()]
        [Range(1, double.MaxValue, ErrorMessage = "El valor de {0} debe ser mayor a 0")]
        public int IdProduct { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "El valor de {0} debe ser mayor a 0")]
        public int Amount { get; set; }
        public decimal Discount { get; set; }
    }
}
