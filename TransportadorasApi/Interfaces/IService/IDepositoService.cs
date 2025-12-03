using TransportadorasApi.Model;

namespace TransportadorasApi.Interfaces.IService
{
    public interface IDepositoService
    {

        public interface IDepositoService
        {
            bool CreateDeposito(Deposito deposito);
            bool UpdateDeposito(int id, Deposito deposito);
            bool DeleteDeposito(int id);
            Deposito GetDeposito(int id);
            ICollection<Deposito> GetDepositos();
            Endereco GetDepositoEndereco(int depositoId);
        }
    }

}

