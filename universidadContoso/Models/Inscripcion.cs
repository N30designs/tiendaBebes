using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace universidadContoso.Models
{

    public enum Grado { A, B, C, D, F }
    public class Inscripcion
    {
        public int inscripcionID { get; set; }
        public int cursoID { get; set; }
        public int estudianteID { get; set; }

        [DisplayFormat(NullDisplayText = "Sin grado")]
        public Grado? grado { get; set; }
        
        public virtual Curso curso { get; set; }
        public virtual Estudiante estudiante { get; set; }

    }
}