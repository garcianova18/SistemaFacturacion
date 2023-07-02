using Facturacion.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Facturacion.Domain.DTOs 
{ 
    public class invoiceDetailsDTO
    {
        public int Id { get; set; }

        //propiedad compuesta para realizar mappeo con Automapper
        [Display(Name = "Product")]
        public string ProductName { get; set; }
        public int Amount { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public decimal Discount { get; set; }
        
    }
}
