using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TiendaBebes.Models
{
    [Table("Productos")]
    public partial class Producto
    {
        public int ID { get; set; }

        [Required, Display(Name = "Producto"), StringLength(100)]
        [RegularExpression(@"^[A-ZÑ]+[a-zA-Z0-9óáúíéüñÑ'\s-]*$", ErrorMessage = "El campo deberá comenzar por mayúsculas y no contener símbolos.")]
        public string Nombre { get; set;}

        [Display(Name = "Descripción"), StringLength(100)]
        [RegularExpression(@"^[A-ZÑ]+[a-zA-Z0-9óáúíéüñÑ'\s-]*$", ErrorMessage = "El campo deberá comenzar por mayúsculas y no contener símbolos.")]
        public string Descripcion { get; set; }

        [Required, Display(Name = "Precio")]
        public decimal Precio { get; set; }
        public int? CategoriaID { get; set; }

        public virtual Categoria Categoria { get; set; }
    }
}