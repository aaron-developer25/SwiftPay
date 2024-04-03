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
		public DateTime Fecha {  get; set; } = DateTime.Now;

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
		[RegularExpression(@"^[a-zA-Z0-9áéíóúÁÉÍÓÚñÑ\s\W]{1,13}$", ErrorMessage = "La cédula debe tener máximo 13 caracteres alfanuméricos, incluyendo símbolos")]
		public string? Cedula { get; set; }

		[Required(ErrorMessage = "Este Campo es Obligatorio")]
        public string? Direccion { get; set; }

        public bool Estado { get; set; }

        public DateTime FechaUltimoPago { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Este Campo es Obligatorio")]
        public float MontoSuplementerio { get; set; }

	    [Required(ErrorMessage = "Este campo es obligatorio")]
	    public string? Bloque {  get; set; }

		[RegularExpression(@"^[A-ZÁÉÍÓÚÑ][a-zA-ZÁÉÍÓÚÑ ]*$", ErrorMessage = "El nombre debe comenzar con una letra mayúscula y no debe contener números ni símbolos.")]
		public string? Asociacion {  get; set; }

    }
}
