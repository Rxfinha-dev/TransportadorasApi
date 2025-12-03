using TransportadorasApi.Data;
using TransportadorasApi.Interfaces.IRepository;
using TransportadorasApi.Model;

namespace TransportadorasApi.Repository
{
    public class RotaRepository : IRotaRepository
    {
        private readonly DataContext _context;
        public RotaRepository(DataContext context)
        {
            _context = context;            
        }

        public bool CreateRota (Rota rota)
        {
          
            _context.Add(rota);

            return Save();
        }

        public bool DeleteRota(Rota rota)
        {
            _context.Remove(rota);
            return Save();
        }

        public Rota GetRota(int id)
        {
            return _context.Rotas.Where(r=>r.Id == id).FirstOrDefault();
        }

        public ICollection<Rota> GetRotas()
        {
            return _context.Rotas.OrderBy(r=>r.Id).ToList();
        }

        public bool RotaExists(int rotaId)
        {
            return _context.Rotas.Any(r=>r.Id == rotaId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateRota(Rota rota)
        {
            _context.Update(rota);
                return Save();
        }
    }
}
