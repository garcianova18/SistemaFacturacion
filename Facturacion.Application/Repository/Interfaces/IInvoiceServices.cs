using Facturacion.Domain.DTOs;
using Facturacion.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facturacion.Application.Repository.Interfaces
{
    public interface IInvoiceServices
    {
        Task<Invoice> GetTotalSubtotalTax(Invoice invoice);
        string CreateInvoiceNumber(int Number);
        Task<Invoice> GetInvoiceByNumber(string InvoiceNumber);
        Task<Invoice> GetInvoiceDetails(int id);
        Task<int> Checkstock(int Id, int Amount);
        Task<ExistsProducDTO> ExistsProduct(Invoice Invoice);
        Task<ExistsProducDTO> ExistsDetails(Invoice invoice);
    }
}
