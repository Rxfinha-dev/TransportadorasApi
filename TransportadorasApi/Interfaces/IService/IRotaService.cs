using TransportadorasApi.Model;

namespace TransportadorasApi.Interfaces.IService
{
    public interface IRotaService
    {
        ICollection<Rota> GetRotas();
        Rota GetRota(int id);


        bool RotaExists(int rotaId);

        bool CreateRota(Rota rota);

        bool DeleteRota(Rota rota);

        bool UpdateRota(Rota rota);
    }
}
