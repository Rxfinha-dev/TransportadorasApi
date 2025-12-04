using TransportadorasApi.Model;

namespace TransportadorasApi.Interfaces.IService
{
    public interface IEnderecoService
    {
        ICollection<Endereco> GetEnderecos();

        Endereco GetEndereco(int id);

        bool CreateEndereco(Endereco endereco);

        bool DeleteEndereco(Endereco endereco);

        bool UpdateEndereco(Endereco endereco);

        bool EnderecoExists(int id);
    }
}
