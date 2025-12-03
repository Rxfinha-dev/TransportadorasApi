using TransportadorasApi.Data;
using TransportadorasApi.Interface;
using TransportadorasApi.Model;

namespace TransportadorasApi.Repository
{
    public class DepositoRepository : IDepositoRepository
    {
        private readonly DataContext _context;
        public DepositoRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateDeposito(Deposito deposito)
        {
            _context.Add(deposito);
            return Save();
        }

        public bool DeleteDeposito(Deposito deposito)
        {
            _context.Remove(deposito);
            return Save();
        }

        public bool DepositoExists(int id)
        {
            return _context.Depositos.Any(d=>d.Id == id);
        }

        public Deposito GetDeposito(int id)
        {
            return _context.Depositos.Where(d => d.Id == id).FirstOrDefault();
        }

        public Deposito GetDepositoByEndereco(int enderecoId)
        {
            return _context.Depositos.Where(d => d.Localizacao.Id == enderecoId).FirstOrDefault();
        }

        public Endereco getDepositoEndereco(int depositoId)
        {
            return _context.Depositos.Where(d=>d.Id == depositoId).Select(d=>d.Localizacao).FirstOrDefault();
        }
        public ICollection<Deposito> GetDepositos()
        {
            return _context.Depositos.OrderBy(d=>d.Id).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateDeposito(Deposito deposito)
        {
            _context.Update(deposito);
            return Save();
        }
    }
}
