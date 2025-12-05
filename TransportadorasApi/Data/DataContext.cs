using Microsoft.EntityFrameworkCore;
using TransportadorasApi.Model;

namespace TransportadorasApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Deposito> Depositos { get; set; }
        public DbSet<Item> Itens { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoItem> PedidoItems { get; set; }
        public DbSet<Rota> Rotas { get; set; }
        public DbSet<Transportadora> Transportadoras { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PedidoItem>()
                .HasKey(pi => new { pi.PedidoId, pi.ItemId });
            modelBuilder.Entity<PedidoItem>()
                .HasOne(p => p.Pedido)
                .WithMany(pi => pi.PedidoItems)
                .HasForeignKey(p => p.PedidoId);
            modelBuilder.Entity<PedidoItem>()
                .HasOne(i => i.Item)
                .WithMany(pi => pi.PedidoItems)
                .HasForeignKey(i=>i.ItemId);    

        }
    }
}
