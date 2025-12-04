using TransportadorasApi.Interfaces.IRepository;
using TransportadorasApi.Interfaces.IService;
using TransportadorasApi.Model;

namespace TransportadorasApi.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IRotaRepository _rotaRepository;
        private readonly IEnderecoRepository _enderecoRepository;

        public PedidoService(IPedidoRepository pedidoRepository, IItemRepository itemRepository, IClienteRepository clienteRepository, IRotaRepository rotaRepository, IEnderecoRepository enderecoRepository)
        {
            _pedidoRepository = pedidoRepository;
            _itemRepository = itemRepository;
            _clienteRepository = clienteRepository;
            _rotaRepository = rotaRepository;
            _enderecoRepository = enderecoRepository;
        }

        public ICollection<Pedido> GetPedidos() =>
            _pedidoRepository.GetPedidos();

        public Pedido GetPedido(int id) =>
            _pedidoRepository.GetPedido(id);

        public ICollection<Item> GetItensByPedido(int pedidoId) =>
            _pedidoRepository.GetItensByPedido(pedidoId);

        public bool PedidoExists(int id) =>
            _pedidoRepository.PedidoExists(id);


    
        public bool CreatePedido(int enderecoOrigemId, int enderecoDestinoId, int clienteId, int rotaId, List<int> itensIds)
        {
            if (!_clienteRepository.ClienteExists(clienteId) ||
                !_rotaRepository.RotaExists(rotaId) ||
                !_enderecoRepository.EnderecoExists(enderecoOrigemId) ||
                !_enderecoRepository.EnderecoExists(enderecoDestinoId))
                return false;

            var pedido = new Pedido
            {
                EnderecoOrigemId = enderecoOrigemId,
                EnderecoDestinoId = enderecoDestinoId,
                Cliente = _clienteRepository.GetCliente(clienteId),
                Rota = _rotaRepository.GetRota(rotaId),
                PedidoItems = itensIds.Select(id => new PedidoItem 
                {
                    ItemId = id,
                    PedidoId = 0
                }).ToList()
            };

            return _pedidoRepository.CreatePedido(pedido);
        }


        
        public bool UpdatePedido(Pedido pedido, List<int> novosItensIds)
        {
            if (!_pedidoRepository.PedidoExists(pedido.Id))
                return false;

            
            pedido.PedidoItems = novosItensIds.Select(i => new PedidoItem
            {
                ItemId = i,
                PedidoId = pedido.Id
            }).ToList();

            return _pedidoRepository.UpdatePedido(pedido);
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
