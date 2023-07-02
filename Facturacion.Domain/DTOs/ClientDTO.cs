
using System.ComponentModel.DataAnnotations;


namespace Facturacion.Domain.DTOs
{
    public class ClientDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; }
        public string Direccion { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; }
        public string Dni { get; set; }

    }
}
