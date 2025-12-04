using TransportadorasApi.Model;

namespace TransportadorasApi.Interfaces.IRepository
{
    public interface IPedidoRepository
    {
        ICollection<Pedido> GetPedidos();
        Pedido GetPedido(int id);
        ICollection<Item> GetItensByPedido(int pedidoId);
        bool PedidoExists(int id);
        bool CreatePedido(Pedido pedido);
        bool UpdatePedido(Pedido pedido);
        bool DeletePedido(Pedido pedido);
        bool Save();

    }
}
