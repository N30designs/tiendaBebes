using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace universidadContoso.Models
{
    public class Curso
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CursoID { get; set; }
        public string Nombre { get; set; }
        public int Creditos { get; set; }

        public virtual ICollection<Inscripcion> Inscripciones { get; set; }

    }
}