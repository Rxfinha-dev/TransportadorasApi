using TransportadorasApi.Model;

namespace TransportadorasApi.Interfaces.IService
{
    public interface IPedidoService
    {
      
        ICollection<Pedido> GetPedidos();
        Pedido GetPedido(int id);
        ICollection<Item> GetItensByPedido(int pedidoId);
             
        bool PedidoExists(int id);      
        bool CreatePedido(
            int enderecoOrigemId,
            int enderecoDestinoId,
            int clienteId,
            int rotaId,
            List<int> itensIds
        );
      
        bool UpdatePedido(
            Pedido pedido,
            List<int> novosItensIds
        );  
        bool DeletePedido(int id);
    }
}
