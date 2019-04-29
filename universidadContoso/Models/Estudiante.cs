using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace universidadContoso.Models
{
    public class Estudiante : Persona
    {
       
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd-MM-yyyy}", ApplyFormatInEditMode =true)]
        [Display(Name = "Fecha de inscripción")]
        public DateTime FechaInscripcion { get; set; }
        
        public virtual ICollection<Inscripcion> Inscripciones { get; set; }
    }
}