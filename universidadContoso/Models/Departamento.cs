using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace universidadContoso.Models
{
    public class Departamento
    {

        public int departamentoID { get; set; }

        [StringLength(50, MinimumLength = 3)]
        public string nombre { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal presupuesto { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de Inicio")]
        public DateTime fechaInicio { get; set; }

        public int? profesorID { get; set; }

        public virtual Profesores administrador { get; set; }
        public virtual ICollection<Curso> Cursos { get; set; }
    }
}