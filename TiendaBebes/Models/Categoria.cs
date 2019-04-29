using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TiendaBebes.Models
{
    public class Categoria
    {
        public int ID { get; set; }
        [Required, Display(Name="Categoria"), StringLength(100)]
        [RegularExpression(@"^[A-ZÑ]+[a-zA-Z0-9óáúíéüñÑ'\s-]*$", ErrorMessage = "El campo deberá comenzar por mayúsculas y no contener símbolos.")]
        public string Nombre { get; set; }
        public virtual ICollection<Producto> Productos { get; set; }
    }
}