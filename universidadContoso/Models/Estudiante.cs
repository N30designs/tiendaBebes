using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace universidadContoso.Models
{
    public class Estudiante
    {
        public int id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "El atributo Apellido no puede contener mas de 50 carácteres")]
        [Display(Name = "Apellidos")]        
        public string apellido { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "El atributo Nombre no puede contener mas de 50 carácteres")]
        [Display(Name = "Nombre")]
        [Column("Nombre")]//En el ejemplo crean esta columna porque crean el campo FirstMidName, en Español esto no tiene sentido por ello
                            // obligo a utilizar la columna pero no realizo cambios en el nombre por lo que en realidad es inútil. Solo lo hago a modo de ejemplo del ejercicio.
        public string nombre { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de Inscripción", Description ="Proporciona una fecha de ingreso")]
        public DateTime fechaInscripcion { get; set; }

        [Display(Name ="Nombre completo")]
        public string nombreCompleto
        {
            get
            {
                return nombre + ", " + apellido;
            }

        }


        public virtual ICollection<Inscripcion> inscripciones { get; set; }


    }
}