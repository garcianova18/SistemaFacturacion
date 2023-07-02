using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Facturacion.Domain.DTOs
{
    public class ClientUpdateDTO
    {
        [Required]
        [Range(1,int.MaxValue, ErrorMessage ="El {0 debe de ser mayor a 0}")]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = null!;
        [Required]
        [StringLength(60)]
        public string LastName { get; set; }

        [Required]
        [StringLength(100)]
        public string Direccion { get; set; } = null!;

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; } = null!;

        [Required]

        [DataType(DataType.EmailAddress)]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Dni { get; set; }
    }
}
