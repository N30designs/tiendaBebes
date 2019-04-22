using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace universidadContoso.Models
{
    public class DespachoAsignado
    {
        [Key]
        [ForeignKey("Profesor")]
        public int ProfesorID { get; set; }
        [StringLength(50)]
        [Display(Name = "Ubicación del despacho")]
        public string Ubicacion { get; set; }

        public virtual Profesor Profesor { get; set; }

    }
}