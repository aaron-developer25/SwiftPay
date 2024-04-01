using System.ComponentModel.DataAnnotations;

namespace SwiftPay.Models
{
	public class Empresa
	{
        [Key]
        public int EmpresaId { get; set; }

        public byte[]? Imagen { get; set; }

        [Required(ErrorMessage = "El campo Nombre es requerido")]
        [RegularExpression(@"^[A-ZÁÉÍÓÚÑ][a-zA-ZÁÉÍÓÚÑ ]*$", ErrorMessage = "El nombre debe comenzar con una letra mayúscula y solo puede contener letras y espacios.")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "El campo RNC es requerido")]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "El RNC debe tener exactamente 9 dígitos")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "El RNC solo puede contener dígitos.")]
        public string? RNC { get; set; }

        [Required(ErrorMessage = "El campo Teléfono es requerido")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "El número de teléfono debe tener exactamente 10 dígitos")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "El número de teléfono solo puede contener dígitos.")]
        public string? Telefono { get; set; }

        [Required(ErrorMessage = "El campo Dirección es requerido")]
        [RegularExpression(@"^[A-ZÁÉÍÓÚÑ][a-zA-ZÁÉÍÓÚÑ0-9 ]*$", ErrorMessage = "La dirección debe comenzar con una letra mayúscula y puede contener letras, números y espacios.")]
        public string? Direccion { get; set; }

        [Required(ErrorMessage = "El campo Correo Electrónico es requerido")]
        [EmailAddress(ErrorMessage = "Introduzca un Correo Electrónico válido")]
        public string? CorreoElectronico { get; set; }

        [Required(ErrorMessage = "El campo Impresora es requerido")]
        public string? Impresora { get; set; }

        public string? NotaFactura { get; set; }
    }
}
