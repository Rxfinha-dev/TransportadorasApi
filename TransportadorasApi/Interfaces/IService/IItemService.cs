using TransportadorasApi.Dto;
using TransportadorasApi.Model;

namespace TransportadorasApi.Interfaces.IService
{
    public interface IItemService
    {
        IEnumerable<Item> GetItens();
        Item GetItem(int id);
        bool ItemExists(int id);

        bool CreateItem(ItemDto dto);
        bool UpdateItem(int id, ItemDto dto);
        bool DeleteItem(int id);
    }
}
