using System.ComponentModel.DataAnnotations;

namespace TiendaBebes.Models
{
    [MetadataType(typeof(ProductoMetada))]
    public partial class Producto
    {
    }

    public class ProductoMetada
    {
        /*ESTO ES UN EJEMPLO DE METADATOS
         * [Required, Display(Name = "Producto"), StringLength(100)]
         *[RegularExpression(@"^[A-ZÑ]+[a-zA-Z0-9óáúíéüñÑ'\s-]*$", ErrorMessage = "El campo deberá comenzar por mayúsculas y no contener símbolos.")]
         *public string Nombre;*/
    }
}