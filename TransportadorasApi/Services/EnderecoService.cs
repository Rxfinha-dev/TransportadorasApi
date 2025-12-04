using Microsoft.AspNetCore.Http.HttpResults;
using TransportadorasApi.Interfaces.IRepository;
using TransportadorasApi.Interfaces.IService;
using TransportadorasApi.Model;

namespace TransportadorasApi.Services
{
    public class EnderecoService : IEnderecoService
    {
        private readonly IEnderecoRepository _enderecoRepository;
        public EnderecoService(IEnderecoRepository enderecoRepository)
        {

            _enderecoRepository = enderecoRepository;
            
        }
        public bool CreateEndereco(Endereco endereco)
        {
            if (_enderecoRepository.EnderecoExists(endereco.Id))
                return false;

            return _enderecoRepository.CreateEndereco(endereco);
        }

        public bool DeleteEndereco(Endereco endereco)
        {
            if(!_enderecoRepository.EnderecoExists(endereco.Id))
                return false;

            return _enderecoRepository.DeleteEndereco(endereco);
        }

        public bool EnderecoExists(int id)
        {
            return _enderecoRepository.EnderecoExists(id);
        }

        public Endereco GetEndereco(int id)
        {
            if (!_enderecoRepository.EnderecoExists(id))
                throw new Exception("Endereço não encontrado");

            return _enderecoRepository.GetEndereco(id);


        }

        public ICollection<Endereco> GetEnderecos()
        {
            return _enderecoRepository.GetEnderecos();
        }

        public bool UpdateEndereco(Endereco endereco)
        {
            if (!_enderecoRepository.EnderecoExists(endereco.Id))
                throw new Exception("Endereço não encontrado");

            return _enderecoRepository.UpdateEndereco(endereco);
        }
    }
}
