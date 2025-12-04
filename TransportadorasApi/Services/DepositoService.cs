using Microsoft.EntityFrameworkCore;
using TransportadorasApi.Data;
using TransportadorasApi.Interfaces.IRepository;
using TransportadorasApi.Interfaces.IService;
using TransportadorasApi.Model;
using TransportadorasApi.Repository;

namespace TransportadorasApi.Services
{
    public class DepositoService : IDepositoService
    {
        private readonly IDepositoRepository _depositoRepository;
        private readonly DataContext _context;
        public DepositoService(IDepositoRepository depositoRepository, DataContext context)
        {
            _depositoRepository = depositoRepository;
            _context = context;
            
        }
        public bool CreateDeposito(Deposito deposito)
        {
            if (_depositoRepository.DepositoExists(deposito.Id))
                return false;

            var enderecoExistente = _context.Enderecos
            .FirstOrDefault(e =>
                e.Rua == deposito.Localizacao.Rua &&
                e.Bairro == deposito.Localizacao.Bairro &&
                e.Cidade == deposito.Localizacao.Cidade &&
                e.cep == deposito.Localizacao.cep &&
                e.Numero == deposito.Localizacao.Numero
            );

            if (enderecoExistente == null)
            {
                return _depositoRepository.CreateDeposito(deposito);
            }
            else
            {
                int enderecoId = enderecoExistente.Id;

                deposito.Localizacao = enderecoExistente;

                return _depositoRepository.CreateDeposito(deposito);
            }
        }

        public bool DeleteDeposito(Deposito deposito)
        {
            if (_depositoRepository.DepositoExists(deposito.Id))
                return false;

            return _depositoRepository.DeleteDeposito(deposito);
        }

        public bool DepositoExists(int depositoId)
        {
            return _depositoRepository.DepositoExists(depositoId);
        }

        public Deposito GetDeposito(int id)
        {
            return _depositoRepository.GetDeposito(id);
        }

        public Deposito GetDepositoByEndereco(int enderecoId)
        {
            return _depositoRepository.GetDepositoByEndereco(enderecoId);
        }

        public IQueryable<Endereco> GetDepositoEndereco(int depositoId)
        {
            return _depositoRepository.getDepositoEndereco(depositoId);
        }

        public ICollection<Deposito> GetDepositos()
        {
            return _depositoRepository.GetDepositos();
        }

        public bool UpdateDeposito(int id, Deposito depositoToUpdate)
        {

            var deposito = _context.Depositos
         .Include(c => c.Localizacao)
         .FirstOrDefault(c => c.Id == id);

            if (deposito == null)
                return false;


            var enderecoExistente = _context.Enderecos.FirstOrDefault(e =>
                e.Rua == depositoToUpdate.Localizacao.Rua &&
                e.Bairro == depositoToUpdate.Localizacao.Bairro &&
                e.Cidade == depositoToUpdate.Localizacao.Cidade &&
                e.cep == depositoToUpdate.Localizacao.cep &&
                e.Numero == depositoToUpdate.Localizacao.Numero
            );

            if (enderecoExistente == null)
            {
                return false;
            }


           deposito.Localizacao = enderecoExistente;

           

            return _depositoRepository.UpdateDeposito(deposito);

        }
    }
}
