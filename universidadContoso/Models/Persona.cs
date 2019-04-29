using System;
using System.ComponentModel.DataAnnotations;

namespace universidadContoso.Models
{
    public class Persona
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        [StringLength(50)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$", ErrorMessage = "El campo deberá comenzar por mayúsculas y no contener símbolos.")]
        public string Nombre { get; set; }


        [Required]
        [Display(Name = "Apellidos")]
        [StringLength(100)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$", ErrorMessage = "El campo deberá comenzar por mayúsculas y no contener símbolos.")]
        public string Apellidos { get; set; }
                       

        [Display(Name = "Nombre completo")]
        public string NombreCompleto
        {
            get { return Nombre + ", " + Apellidos; }
        }
    }
}