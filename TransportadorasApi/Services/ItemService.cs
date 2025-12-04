using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using TransportadorasApi.Interfaces.IRepository;
using TransportadorasApi.Interfaces.IService;
using TransportadorasApi.Model;
using TransportadorasApi.Repository;

namespace TransportadorasApi.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }
        public bool CreateItem(Item item)
        {
            if(!_itemRepository.ItemExists(item.Id))
                return false;

            return _itemRepository.CreateItem(item);                
            
        }

        public bool DeleteItem(Item item)
        {
            if(!_itemRepository.ItemExists(item.Id))
                return false;
            return _itemRepository.DeleteItem(item); 
        }

        public Item GetItem(int id)
        { 
            return _itemRepository.GetItem(id);
        }

        public ICollection<Item> GetItens()
        {
            return _itemRepository.GetItens();
        }

        public ICollection<Pedido> GetPedidosByItem(int itemId)
        {
            return _itemRepository.GetPedidosByItem(itemId);
        }

        public bool ItemExists(int id)
        {
            return _itemRepository.ItemExists(id);
        }
  

        public bool UpdateItem(Item item)
        {
            if (!_itemRepository.ItemExists(item.Id))
                return false;

            return _itemRepository.UpdateItem(item);
        }
    }
}
