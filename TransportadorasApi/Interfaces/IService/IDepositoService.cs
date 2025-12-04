using TransportadorasApi.Model;

namespace TransportadorasApi.Interfaces.IService
{
    public interface IDepositoService
    {

       
            bool CreateDeposito(Deposito deposito);
            bool DepositoExists(int depositoId);
            bool UpdateDeposito(int id, Deposito deposito);
            bool DeleteDeposito(Deposito deposito);
            Deposito GetDeposito(int id);
            ICollection<Deposito> GetDepositos();
            Deposito GetDepositoByEndereco(int enderecoId);
            IQueryable<Endereco> GetDepositoEndereco(int depositoId);
        
    }

}

