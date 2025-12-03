using Microsoft.AspNetCore.Components.Web;
using TransportadorasApi.Model;

namespace TransportadorasApi.Interfaces.IRepository
{
    public interface IEnderecoRepository
    {
        ICollection<Endereco> GetEnderecos();

        Endereco GetEndereco(int id);

        bool CreateEndereco (Endereco endereco);

        bool DeleteEndereco(Endereco endereco);

        bool UpdateEndereco(Endereco endereco);

        bool EnderecoExists(int id);
        bool Save();
    }
}
