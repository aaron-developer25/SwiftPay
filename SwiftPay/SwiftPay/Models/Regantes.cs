using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SwiftPay.Models
{
    public class Regantes
    {
        [Key]
        public int ReganteId { get; set; }

        public string? CodigoRegante { get; set; }

        [Required(ErrorMessage = "Este Campo es Obligatorio")]
        public string? Usuario { get; set; }

        [Required(ErrorMessage = "Este Campo es Obligatorio")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "La contraseña debe tener al menos 8 caracteres, incluyendo al menos una letra mayúscula, una letra minúscula, un número y un símbolo.")]
        public string? Contrasena { get; set; }

        [Required(ErrorMessage = "Este Campo es Obligatorio")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "Este Campo es Obligatorio")]
        public string? Apellido { get; set; }

        public string? Apodo { get; set; }

        [Required(ErrorMessage = "Este Campo es Obligatorio")]
        [EmailAddress(ErrorMessage = "Introduzca un Correo Electrónico válido")]
        public string? CorreoElectronico { get; set; }

        [Required(ErrorMessage = "Este Campo es Obligatorio")]
        public string? Telefono { get; set; }

        [Required(ErrorMessage = "Este Campo es Obligatorio")]
        public string? Nacionalidad { get; set; }

        [Required(ErrorMessage = "Este Campo es Obligatorio")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "La cédula debe tener exactamente 11 dígitos")]
        public string? Cedula { get; set; }

        [Required(ErrorMessage = "Este Campo es Obligatorio")]
        public string? Direccion { get; set; }

        public bool Estado { get; set; }

        public DateTime FechaUltimoPago { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Este Campo es Obligatorio")]
        public float MontoSuplementerio { get; set; }

    }
}
