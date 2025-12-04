using TransportadorasApi.Model;

namespace TransportadorasApi.Interfaces.IService
{
    public interface IPedidoService
    {
        ICollection<Pedido> GetPedidos();
        Pedido GetPedido(int id);
        ICollection<Item> GetItensByPedido(int pedidoId);

        bool PedidoExists(int id);

        bool CreatePedido(Pedido pedido, List<int> itensIds);
        bool UpdatePedido(Pedido pedido, List<int> itensIds);
        bool DeletePedido(int id);
    }
}
