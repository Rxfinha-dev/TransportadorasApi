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
       

        public bool CreatePedido(Pedido pedido)
        {
            _context.Add(pedido);
            return Save();
        }

        public bool DeletePedido(Pedido pedido)
        {
            _context.Remove(pedido);
            return Save();
        }

        public ICollection<Item> GetItensByPedido(int pedidoId)
        {
            return _context.PedidoItems.Where(pi => pi.PedidoId == pedidoId).Select(i => i.Item).ToList();
        }

        public Pedido GetPedido(int id)
        {
            return _context.Pedidos.Where(p => p.Id == id).FirstOrDefault();
        }

        public ICollection<Pedido> GetPedidos()
        {
            return _context.Pedidos.OrderBy(p => p.Id).ToList();
        }

        public bool PedidoExists(int id)
        {
            return _context.Pedidos.Any(p => p.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false; ;
        }

        public bool UpdatePedido(Pedido pedido)
        {
            _context.Update(pedido);
            return Save();
        }
    }
}
