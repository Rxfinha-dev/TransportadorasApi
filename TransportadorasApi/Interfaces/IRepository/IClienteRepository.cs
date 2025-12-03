using TransportadorasApi.Model;

namespace TransportadorasApi.Interfaces.IRepository
{
    public interface IClienteRepository
    {
        ICollection<Cliente> GetClientes();
        Cliente GetCliente(string cpf);

        Cliente GetCliente(int id);

        bool CreateCliente(Cliente cliente);
        bool UpdateCliente(int clienteId, Cliente cliente);
        bool DeleteCliente(Cliente cliente);

        bool ClienteExists(int id);

        bool ClienteExistsByCpf(string cpf);

        bool Save();
    }
}
