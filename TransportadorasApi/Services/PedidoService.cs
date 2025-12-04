using TransportadorasApi.Interfaces.IRepository;
using TransportadorasApi.Interfaces.IService;
using TransportadorasApi.Model;

namespace TransportadorasApi.Service
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IItemRepository _itemRepository;

        public PedidoService(IPedidoRepository pedidoRepository, IItemRepository itemRepository)
        {
            _pedidoRepository = pedidoRepository;
            _itemRepository = itemRepository;
        }

        public ICollection<Pedido> GetPedidos()
        {
            return _pedidoRepository.GetPedidos();
        }

        public Pedido GetPedido(int id)
        {
            return _pedidoRepository.GetPedido(id);
        }

        public ICollection<Item> GetItensByPedido(int pedidoId)
        {
            return _pedidoRepository.GetItensByPedido(pedidoId);
        }

        public bool PedidoExists(int id)
        {
            return _pedidoRepository.PedidoExists(id);
        }

        public bool CreatePedido(Pedido pedido, List<int> itensIds)
        {
            pedido.PedidoItems = itensIds.Select(itemId => new PedidoItem
            {
                ItemId = itemId,
                Pedido = pedido
            }).ToList();

            return _pedidoRepository.CreatePedido(pedido);
        }

        public bool UpdatePedido(Pedido pedido, List<int> itensIds)
        {
            var pedidoDb = _pedidoRepository.GetPedido(pedido.Id);

            if (pedidoDb == null)
                return false;

            pedidoDb.EnderecoOrigemId = pedido.EnderecoOrigemId;
            pedidoDb.EnderecoDestinoId = pedido.EnderecoDestinoId;
            pedidoDb.ValorTotal = pedido.ValorTotal;

            pedidoDb.ClienteId = pedido.ClienteId;
            pedidoDb.RotaId = pedido.RotaId;

            pedidoDb.PedidoItems.Clear();

            pedidoDb.PedidoItems = itensIds.Select(itemId => new PedidoItem
            {
                PedidoId = pedido.Id,
                ItemId = itemId
            }).ToList();

            return _pedidoRepository.UpdatePedido(pedidoDb);
        }

        public bool DeletePedido(int id)
        {
            var pedido = _pedidoRepository.GetPedido(id);
            if (pedido == null)
                return false;

            return _pedidoRepository.DeletePedido(pedido);
        }
    }
}
