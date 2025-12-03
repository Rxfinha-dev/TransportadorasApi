 using Microsoft.EntityFrameworkCore;
using TransportadorasApi.Data;
using TransportadorasApi.Interfaces.IRepository;
using TransportadorasApi.Model;

namespace TransportadorasApi.Repository
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly DataContext _context;
        public EnderecoRepository(DataContext context)
        {
            _context = context;
            
        }
        public bool CreateEndereco(Endereco endereco)
        {
            _context.Add(endereco);
            return Save();
        }

        public bool DeleteEndereco(Endereco endereco)
        {
            _context.Remove(endereco);
            return Save();
        }

        public bool UpdateEndereco(Endereco endereco)
        {
            _context.Update(endereco);
            return Save();
        }

        public bool EnderecoExists(int enderecoId)
        {
            return _context.Enderecos.Any(p => p.Id == enderecoId);
        }

        public Endereco GetEndereco(int id)
        {
            return _context.Enderecos.Where(p => p.Id == id).FirstOrDefault();
        }

        public ICollection<Endereco> GetEnderecos()
        {
            return _context.Enderecos.OrderBy(p => p.Id).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
