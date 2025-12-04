using AutoMapper;
using TransportadorasApi.Dto;
using TransportadorasApi.Interfaces.IRepository;
using TransportadorasApi.Interfaces.IService;
using TransportadorasApi.Model;

namespace TransportadorasApi.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IMapper _mapper;

        public ItemService(IItemRepository itemRepository, IMapper mapper)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
        }

        public IEnumerable<Item> GetItens()
        {
            return _itemRepository.GetItens();
        }

        public Item GetItem(int id)
        {
            return _itemRepository.GetItem(id);
        }

        public bool ItemExists(int id)
        {
            return _itemRepository.ItemExists(id);
        }

        public bool CreateItem(ItemDto dto)
        {
            var item = _mapper.Map<Item>(dto);
            return _itemRepository.CreateItem(item);
        }

        public bool UpdateItem(int id, ItemDto dto)
        {
            if (!_itemRepository.ItemExists(id))
                return false;

            var item = _itemRepository.GetItem(id);

            _mapper.Map(dto, item);

            return _itemRepository.UpdateItem(item);
        }

        public bool DeleteItem(int id)
        {
            if (!_itemRepository.ItemExists(id))
                return false;

            var item = _itemRepository.GetItem(id);

            return _itemRepository.DeleteItem(item);
        }
    }
}
