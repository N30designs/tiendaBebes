using System.Data.Entity;
using TiendaBebes.Models;

namespace TiendaBebes.DAL
{
    public class TiendaContext : DbContext
    {
        public DbSet<Producto> Productos{ get; set; }
        public DbSet<Categoria> Categorias { get; set; }
    }
}