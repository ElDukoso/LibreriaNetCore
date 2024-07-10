using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Libreria.Models;

namespace Libreria.Data
{
    public class LibreriaContext : IdentityDbContext<Usuario>
    {
        public LibreriaContext(DbContextOptions<LibreriaContext> options) : base(options)
        {
        }

        public DbSet<Libro> Libros { get; set; }
        public DbSet<CarritoDeCompra> CarritoDeCompras { get; set; }
        public DbSet<ItemCarrito> ItemCarritos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<DetallePedido> DetallePedidos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CarritoDeCompra>()
                .HasMany(c => c.Items)
                .WithOne(i => i.Carrito)
                .HasForeignKey(i => i.CarritoId);

            modelBuilder.Entity<Pedido>()
                .HasMany(o => o.Detalles)
                .WithOne(d => d.Pedido)
                .HasForeignKey(d => d.PedidoId);
        }
    }
}
