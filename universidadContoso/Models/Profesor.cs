using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace universidadContoso.Models
{
    public class Profesor : Persona
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de Contratación")]
        public DateTime FechaContratacion { get; set; }


        public virtual ICollection<Curso> Cursos{ get; set; }
        public virtual DespachoAsignado DespachoAsignado{ get; set; }
    }
}