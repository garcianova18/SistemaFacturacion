using System;
using System.Collections.Generic;

namespace Facturacion.Domain.Models
{
    public partial class Invoice
    {
        public Invoice()
        {
            InvoiceDetails = new HashSet<InvoiceDetail>();
        }

        public int Id { get; set; }
        public string Ninvoice { get; set; }
        public DateTime DateCreate { get; set; }
        public int IdClient { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Itbis { get; set; }
        public decimal Total { get; set; }
        public int IdUser { get; set; }

        public virtual Client Client { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
