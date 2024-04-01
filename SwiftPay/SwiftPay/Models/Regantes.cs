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
        [RegularExpression(@"^[A-ZÁÉÍÓÚÑ][a-zA-ZÁÉÍÓÚÑ0-9]*$", ErrorMessage = "El usuario debe comenzar con una letra mayúscula y solo puede contener letras y números.")]
        public string? Usuario { get; set; }

        [Required(ErrorMessage = "Este Campo es Obligatorio")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$", ErrorMessage = "La contraseña debe tener al menos 8 caracteres, incluyendo al menos una letra mayúscula, una letra minúscula, un número y un símbolo.")]
        public string? Contrasena { get; set; }

        [Required(ErrorMessage = "Este Campo es Obligatorio")]
        [RegularExpression(@"^[A-ZÁÉÍÓÚÑ][a-zA-ZÁÉÍÓÚÑ ]*$", ErrorMessage = "El nombre debe comenzar con una letra mayúscula y solo puede contener letras y espacios.")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "Este Campo es Obligatorio")]
        [RegularExpression(@"^[A-ZÁÉÍÓÚÑ][a-zA-ZÁÉÍÓÚÑ ]*$", ErrorMessage = "El apellido debe comenzar con una letra mayúscula y solo puede contener letras y espacios.")]
        public string? Apellido { get; set; }

        public string? Apodo { get; set; }

        [Required(ErrorMessage = "Este Campo es Obligatorio")]
        [EmailAddress(ErrorMessage = "Introduzca un Correo Electrónico válido")]
        public string? CorreoElectronico { get; set; }

        [Required(ErrorMessage = "Este Campo es Obligatorio")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "El número de teléfono debe tener exactamente 9 dígitos y no puede contener letras.")]
        public string? Telefono { get; set; }

        [Required(ErrorMessage = "Este Campo es Obligatorio")]
        [RegularExpression(@"^[A-ZÁÉÍÓÚÑ][a-zA-ZÁÉÍÓÚÑ ]*$", ErrorMessage = "La nacionalidad debe comenzar con una letra mayúscula y solo puede contener letras y espacios.")]
        public string? Nacionalidad { get; set; }

        [Required(ErrorMessage = "Este Campo es Obligatorio")]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "La cédula debe tener exactamente 11 dígitos")]
        public string? Cedula { get; set; }

        [Required(ErrorMessage = "Este Campo es Obligatorio")]
        public string? Direccion { get; set; }

        [RegularExpression(@"^[A-ZÁÉÍÓÚÑ][a-zA-ZÁÉÍÓÚÑ]*$", ErrorMessage = "El estado debe comenzar con una letra mayúscula y solo puede contener letras.")]
        public bool Estado { get; set; }

        public DateTime FechaUltimoPago { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Este Campo es Obligatorio")]
        [Range(0, float.MaxValue, ErrorMessage = "El monto suplementario no puede ser negativo")]
        public float MontoSuplementerio { get; set; }

    }
}
