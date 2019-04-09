using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace universidadContoso.Models
{
    public class DespachoAsignacion
    {
       [Key]
       [ForeignKey("Profesor")]
       public int profesorID { get; set; }
        
       [StringLength(50)]
       [Display(Name ="Ubicación de oficina")]
       public string localizacion { get; set; }

       public virtual Profesores profesor { get; set; }

    }
}