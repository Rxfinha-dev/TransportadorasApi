using TransportadorasApi.Interfaces.IRepository;
using TransportadorasApi.Interfaces.IService;
using TransportadorasApi.Model;

namespace TransportadorasApi.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;

        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public ICollection<Item> GetItens()
        {
            return _itemRepository.GetItens();
        }

        public Item GetItem(int id)
        {
            return _itemRepository.GetItem(id);
        }

        public ICollection<Pedido> GetPedidosByItem(int itemId)
        {
            return _itemRepository.GetPedidosByItem(itemId);
        }

        public bool ItemExists(int id)
        {
            return _itemRepository.ItemExists(id);
        }

        public bool CreateItem(Item item)
        {
            return _itemRepository.CreateItem(item);
        }

        public bool UpdateItem(Item item)
        {
            return _itemRepository.UpdateItem(item);
        }

        public bool DeleteItem(Item item)
        {
            return _itemRepository.DeleteItem(item);
        }
    }
}
