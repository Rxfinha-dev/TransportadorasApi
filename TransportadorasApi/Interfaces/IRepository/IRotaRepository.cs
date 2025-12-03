using TransportadorasApi.Model;

namespace TransportadorasApi.Interfaces.IRepository
{
    public interface IRotaRepository
    {
        ICollection<Rota> GetRotas();
        Rota GetRota(int id);

     
        bool RotaExists(int rotaId);

        bool CreateRota(Rota rota);

        bool DeleteRota(Rota rota);

        bool UpdateRota(Rota rota);
        bool Save();
    }
}
