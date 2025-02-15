using Microsoft.EntityFrameworkCore;
using NegocioGalRio_API.Models;

namespace NegocioGalRio_API.Contexts
{
    public class NegocioContext : DbContext
    {
        public NegocioContext(DbContextOptions<NegocioContext> options) : base(options) { }

        public virtual DbSet<Producto> Productos { get; set; } 
        public virtual DbSet<Categoria> Categorias { get; set; } 
        public virtual DbSet<Rol> Roles { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }
        public virtual DbSet<Compra> Compras { get; set; }
        public virtual DbSet<DetalleCompra> DetalleCompras { get; set; }
        public virtual DbSet<DetalleVenta> DetalleVentas { get; set; }
        public virtual DbSet<Proveedor>  Proveedors { get; set; }
        public virtual DbSet<Venta> Ventas { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.Roles)
                .WithMany(r => r.Usuarios)
                .UsingEntity(j => j.ToTable("UsuarioRol"));
        }
    }
}
