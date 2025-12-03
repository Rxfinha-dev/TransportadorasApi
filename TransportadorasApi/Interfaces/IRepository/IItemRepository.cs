using TransportadorasApi.Model;

namespace TransportadorasApi.Interfaces.IRepository
{
    public interface IItemRepository
    {
        ICollection<Item> GetItens();
        Item GetItem(int id);

        ICollection<Pedido> GetPedidosByItem(int itemId);
        
        bool CreateItem(Item item);

        bool UpdateItem(Item item);
        bool DeleteItem(Item item);

        bool ItemExists(int id);

        bool Save();



    }
}
