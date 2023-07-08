using Facturacion.Domain.DTOs;
using Facturacion.Domain.Models;

namespace Facturacion.Application.Repository.Interfaces
{
    public interface IInvoiceServices
    {
        Task<Invoice> GetTotalSubtotalTax(Invoice invoice);
        string CreateInvoiceNumber(int Number);
        Task<Invoice> GetInvoiceByNumber(string InvoiceNumber);
        Task<Invoice> GetInvoiceDetails(int id);
        Task<bool> Checkstock(int Id, int Amount);
        Task<ExistsProducDTO> ExistsProduct(Invoice Invoice);
        Task<ExistsProducDTO> ExistsDetails(Invoice invoice);
        Task<Invoice> GetInvoiceAsNotraking(int id);
    }
}
