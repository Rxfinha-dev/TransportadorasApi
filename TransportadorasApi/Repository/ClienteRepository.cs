using Microsoft.EntityFrameworkCore;
using TransportadorasApi.Data;
using TransportadorasApi.Interfaces.IRepository;
using TransportadorasApi.Model;

namespace TransportadorasApi.Repository
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly DataContext _context;
        public ClienteRepository(DataContext context)
        {
            _context = context;
        }
        public bool ClienteExists(int id)
        {
            return _context.Clientes.Any(c=>c.Id == id);
        }

        public bool ClienteExistsByCpf(string cpf)
        {
            return _context.Clientes.Any(c=>c.Cpf.ToUpper() == cpf.ToUpper());
        }

        public bool CreateCliente(Cliente cliente)
        {
            _context.Add(cliente);
            return Save();
        }

        public bool DeleteCliente(Cliente cliente)
        {
            _context.Remove(cliente);
            return Save();
        }

        public Cliente GetCliente(string cpf)
        {
            return _context.Clientes.Include(c => c.endereco).Where(c => c.Cpf == cpf).FirstOrDefault();

        }

        public Cliente GetCliente(int id)
        {
            return _context.Clientes.Include(c=>c.endereco).Where(c=>c.Id == id).FirstOrDefault();
        }

        public ICollection<Cliente> GetClientes()
        {
            return _context.Clientes.Include(c=>c.endereco).OrderBy(c => c.Id).ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCliente(int clienteId, Cliente cliente)
        {
           _context.Update(cliente);
            return Save();
        }
    }
}
