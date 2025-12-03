using TransportadorasApi.Model;

namespace TransportadorasApi.Interfaces.IRepository
{
    public interface IDepositoRepository
    {
        ICollection<Deposito> GetDepositos();
        Deposito GetDeposito(int id);
        Deposito GetDepositoByEndereco(int enderecoId);
        IQueryable<Endereco> getDepositoEndereco(int depositoId);
        bool DepositoExists(int id);

        bool UpdateDeposito(int dpositoId, Deposito deposito);

        bool DeleteDeposito(Deposito deposito);

        bool CreateDeposito(Deposito deposito);

        bool Save();


    }
}
