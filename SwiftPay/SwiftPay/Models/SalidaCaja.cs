using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftPay.Models
{
    public class SalidaCaja
    {
        [Key]
        public int CajaId { get; set; }

        [Required(ErrorMessage = "El campo UsuarioId es obligatorio")]
        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "El campo Monto es obligatorio")]
        [Range(0, float.MaxValue, ErrorMessage = "El monto no puede ser negativo")]
        public float Monto { get; set; }

        [Required(ErrorMessage = "El campo Asunto es obligatorio")]
        [RegularExpression(@"^[A-ZÁÉÍÓÚÑ].*$", ErrorMessage = "El asunto debe comenzar con una letra mayúscula")]
        public string? Asunto { get; set; }

        [Required(ErrorMessage = "El campo Nota es obligatorio")]
        [RegularExpression(@"^[A-ZÁÉÍÓÚÑ].*$", ErrorMessage = "La nota debe comenzar con una letra mayúscula")]
        public string? Nota { get; set; }

        [Required(ErrorMessage = "El campo Fecha es obligatorio")]
        public DateTime Fecha { get; set; } = DateTime.Now;

    }
}
