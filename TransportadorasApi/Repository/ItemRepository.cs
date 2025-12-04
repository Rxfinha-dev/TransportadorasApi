using TransportadorasApi.Data;
using TransportadorasApi.Interfaces.IRepository;
using TransportadorasApi.Model;
using Microsoft.EntityFrameworkCore;

namespace TransportadorasApi.Repository
{
    public class ItemRepository : IItemRepository
    {
        private readonly DataContext _context;

        public ItemRepository(DataContext context)
        {
            _context = context;
        }

        public bool ItemExists(int id)
        {
            return _context.Itens.Any(i => i.Id == id);
        }

        public ICollection<Item> GetItens()
        {
            return _context.Itens
                .OrderBy(i => i.Id)
                .ToList();
        }

        public Item GetItem(int id)
        {
            return _context.Itens
                .FirstOrDefault(i => i.Id == id);
        }

        public ICollection<Pedido> GetPedidosByItem(int itemId)
        {
            return _context.PedidoItems
                .Where(pi => pi.ItemId == itemId)
                .Include(pi => pi.Pedido)
                .Select(pi => pi.Pedido)
                .ToList();
        }

        public bool CreateItem(Item item)
        {
            _context.Add(item);
            return Save();
        }

        public bool UpdateItem(Item item)
        {
            _context.Update(item);
            return Save();
        }

        public bool DeleteItem(Item item)
        {
            _context.Remove(item);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
