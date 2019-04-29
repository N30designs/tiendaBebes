using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace universidadContoso.Models
{
    public class Departamento
    {
        public int DepartamentoID { get; set; }
        [StringLength(50, MinimumLength =3)]
        public string Nombre { get; set; }
        
        [DataType(DataType.Currency)]
        [Column(TypeName= "money")]
        public decimal Presupuesto { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode =true)]
        [Display(Name = "Fecha de Inicio")]
        public DateTime FechaInicio { get; set; }

        public int? ProfesorID { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        public virtual Profesor Administrador { get; set; }
        public virtual ICollection<Curso> Cursos { get; set; }

    }
}