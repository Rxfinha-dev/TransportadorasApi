using TransportadorasApi.Model;

namespace TransportadorasApi.Interfaces.IRepository
{
    public interface IItemRepository
    {
        ICollection<Item> GetItens();
        Item GetItem(int id);
        bool ItemExists(int id);
        bool CreateItem(Item item);
        bool UpdateItem(Item item);
        bool DeleteItem(Item item);
        ICollection<Pedido> GetPedidosByItem(int itemId);
        bool Save();
    }
}
