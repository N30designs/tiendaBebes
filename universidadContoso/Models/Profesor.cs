using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace universidadContoso.Models
{
    public class Profesor
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Nombre")]
        [StringLength(50)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$", ErrorMessage = "El campo deberá comenzar por mayúsculas y no contener símbolos.")]
        public string Nombre{ get; set; }


        [Required]
        [Display(Name = "Apellidos")]
        [StringLength(100)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$", ErrorMessage = "El campo deberá comenzar por mayúsculas y no contener símbolos.")]
        public string Apellidos { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de Contratación")]
        public DateTime FechaContratacion { get; set; }

        [Display(Name = "Nombre completo")]
        public string NombreCompleto
        {
            get { return Apellidos+ ", " + Nombre; }
        }

        public virtual ICollection<Curso> Cursos{ get; set; }
        public virtual DespachoAsignado DespachoAsignado{ get; set; }
    }
}