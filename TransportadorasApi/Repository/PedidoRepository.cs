using Microsoft.EntityFrameworkCore;
using TransportadorasApi.Data;
using TransportadorasApi.Interfaces.IRepository;
using TransportadorasApi.Model;

namespace TransportadorasApi.Repository
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly DataContext _context;

        public PedidoRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Pedido> GetPedidos()
        {
            return _context.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.Rota)
                .Include(p => p.PedidoItems)
                .ThenInclude(pi => pi.Item)
                .OrderBy(p => p.Id)
                .ToList();
        }

        public Pedido GetPedido(int id)
        {
            return _context.Pedidos
                .Include(p => p.Cliente)
                .Include(p => p.Rota)
                .Include(p => p.PedidoItems)
                .ThenInclude(pi => pi.Item)
                .FirstOrDefault(p => p.Id == id);
        }

        public bool PedidoExists(int id)
        {
            return _context.Pedidos.Any(p => p.Id == id);
        }

        public ICollection<Item> GetItensByPedido(int pedidoId)
        {
            return _context.PedidoItems
                .Where(pi => pi.PedidoId == pedidoId)
                .Select(pi => pi.Item)
                .ToList();
        }

        public bool CreatePedido(Pedido pedido)
        {
            _context.Add(pedido);
            return Save();
        }

        public bool UpdatePedido(Pedido pedido)
        {
            _context.Update(pedido);
            return Save();
        }

        public bool DeletePedido(Pedido pedido)
        {
            _context.Remove(pedido);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
