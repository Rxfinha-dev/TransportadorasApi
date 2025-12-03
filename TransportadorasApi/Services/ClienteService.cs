using Microsoft.EntityFrameworkCore;
using TransportadorasApi.Data;
using TransportadorasApi.Interfaces.IRepository;
using TransportadorasApi.Interfaces.IService;
using TransportadorasApi.Model;

namespace TransportadorasApi.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly DataContext _context;
        public ClienteService (IClienteRepository clienteRepository, DataContext context)
        {
            _clienteRepository = clienteRepository;
            _context = context;
        }
        public bool ClienteExists(int id)
        {
            return _clienteRepository.ClienteExists(id);
        }

        public bool ClienteExistsByCpf(string cpf)
        {
            return _clienteRepository.ClienteExistsByCpf(cpf);

        }

        public bool CreateCliente(Cliente cliente)
        {
            var enderecoExistente = _context.Enderecos
            .FirstOrDefault(e =>
                e.Rua == cliente.endereco.Rua &&
                e.Bairro == cliente.endereco.Bairro &&
                e.Cidade == cliente.endereco.Cidade &&
                e.cep == cliente.endereco.cep &&
                e.Numero == cliente.endereco.Numero
            );

            if (enderecoExistente == null)
            {
              return  _clienteRepository.CreateCliente(cliente);
            }
            else
            {
                int enderecoId = enderecoExistente.Id;




                cliente.endereco = enderecoExistente;
                

                return _clienteRepository.CreateCliente(cliente);
            }
           
        }

        public bool DeleteCliente(Cliente cliente)
        {
          return _clienteRepository.DeleteCliente(cliente);

        }

        public Cliente GetCliente(string cpf)
        {
            return _clienteRepository.GetCliente(cpf);
        }

        public Cliente GetCliente(int id)
        {
            return _clienteRepository.GetCliente(id);
        }

        public ICollection<Cliente> GetClientes()
        {
            return _clienteRepository.GetClientes();
        }


        public bool UpdateCliente(int clienteId, Cliente clienteToUpdate)
        {
            var cliente = _context.Clientes
        .Include(c => c.endereco)
        .FirstOrDefault(c => c.Id == clienteId);

            if (cliente == null)
                return false;

          
            var enderecoExistente = _context.Enderecos.FirstOrDefault(e =>
                e.Rua == clienteToUpdate.endereco.Rua &&
                e.Bairro == clienteToUpdate.endereco.Bairro &&
                e.Cidade == clienteToUpdate.endereco.Cidade &&
                e.cep == clienteToUpdate.endereco.cep &&
                e.Numero == clienteToUpdate.endereco.Numero
            );

            if (enderecoExistente == null)
            {
                return false;
            }

   
            cliente.Name = clienteToUpdate.Name;
            cliente.Cpf = clienteToUpdate.Cpf;
            cliente.Number = clienteToUpdate.Number;

          
            cliente.endereco = enderecoExistente;

            return _clienteRepository.UpdateCliente(clienteId, cliente);

        }
    }
}
