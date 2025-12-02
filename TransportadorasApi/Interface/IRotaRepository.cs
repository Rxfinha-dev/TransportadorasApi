using TransportadorasApi.Model;

namespace TransportadorasApi.Interface
{
    public interface IRotaRepository
    {
        ICollection<Rota> GetRotas();
        Rota GetRota(int id);

        Rota GetRota(string nome);
        bool RotaExists(int rotaId);

        bool CreateRota(Rota rota);

        bool DeleteRota(Rota rota);

        bool UpdateRota(Rota rota);
        bool Save();
    }
}
