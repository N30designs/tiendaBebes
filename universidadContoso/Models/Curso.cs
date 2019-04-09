using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace universidadContoso.Models
{
    public class Curso
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Número")]
        public int cursoID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string titulo { get; set; }

        [Range(0,5)]
        public int creditos { get; set; }

        public int departamentoID { get; set; }

        public virtual Departamento departamento { get; set; }
        public virtual ICollection<Inscripcion> inscripciones { get; set; }
        public virtual ICollection<Profesores> profesor { get; set; }

        

    }
}