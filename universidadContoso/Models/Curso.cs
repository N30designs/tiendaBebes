using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace universidadContoso.Models
{
    public class Curso
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Número")]
        public int CursoID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string Nombre { get; set; }

        [Range(0, 5)]
        public int Creditos { get; set; }

        public int DepartamentoID { get; set; }

        public virtual Departamento Departamento { get; set; }

        public virtual ICollection<Inscripcion> Inscripciones { get; set; }
        public virtual ICollection<Profesor> Profesores { get; set; }

    }
}