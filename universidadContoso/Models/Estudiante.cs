using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace universidadContoso.Models
{
    public class Estudiante
    {
        public int ID { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Este campo no puede contener mas de 50 carácteres")]
        /*La siguiente expresión regular obligará a que el campo comience por mayúscula y solo contenga leras o simbolos comunes,
        he añadido el parametro ErrorMessage para especificar la retroalimentación.*/
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$",ErrorMessage ="El campo deberá comenzar por mayúsculas y no contener símbolos.")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Este campo no puede contener mas de 50 carácteres.")]
        /*La siguiente expresión regular obligará a que el campo comience por mayúscula y solo contenga leras o simbolos comunes,
        he añadido el parametro ErrorMessage para especificar la retroalimentación.*/
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$", ErrorMessage = "El campo deberá comenzar por mayúsculas y no contener símbolos.")]
        //El nombre de la columna en la base de datos puede ser cambiado utilizando la notación [Column=("NOMBRE")]
        [Display(Name = "Apellidos")]
        public string Apellidos { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd-MM-yyyy}", ApplyFormatInEditMode =true)]
        [Display(Name = "Fecha de inscripción")]
        public DateTime FechaInscripcion { get; set; }

        [Display(Name = "Nombre completo")]
        public string NombreCompleto
        {
            get
            {
                return Nombre + ", " + Apellidos;
            }
        }

        public virtual ICollection<Inscripcion> Inscripciones { get; set; }
    }
}