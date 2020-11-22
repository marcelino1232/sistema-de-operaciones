using Entidad;

namespace Datos
{
    public class Repositorio 
    {
        public DbSet<Persona> persona { get; set; } = new DbSet<Persona>();
        public DbSet<Sexo> sexo { get; set; } = new DbSet<Sexo>();
        public DbSet<Venta> venta { get; set; } = new DbSet<Venta>();
        public DbSet<Producto> producto { get; set; } = new DbSet<Producto>();
        public DbSet<Cliente> cliente { get; set; } = new DbSet<Cliente>();
    }
}
