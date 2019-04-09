using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace universidadContoso.Models
{
    public class Profesores
    {
        public int id { get; set; }

        [Required]
        [Column("Nombre")]
        [Display(Name = "Nombre")]
        [StringLength(50)]
        public string nombre { get; set; }

        [Required]
        [Display(Name = "Apellidos")]
        [StringLength(100)]//Aumentado desde 50 en el ejemplo dado que en nuestro caso incluye los dos apellidos.
        public string apellidos {get;set;}

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Fecha de Contratación")]
        public DateTime fechaContratacion { get; set; }

        [Display(Name="Nombre Completo")]
        public string nombreCompleto
        {
            get { return nombre + ", " + apellidos ; }
        }

        public virtual ICollection<Curso> Cursos { get; set; }
        public virtual DespachoAsignacion despachoAsignacion { get; set; }






    }
}