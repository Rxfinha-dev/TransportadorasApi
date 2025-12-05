using TransportadorasApi.Interfaces.IRepository;
using TransportadorasApi.Interfaces.IService;
using TransportadorasApi.Model;

namespace TransportadorasApi.Services
{
    public class RotaService : IRotaService
    {
        private readonly IRotaRepository _rotaRepository;
        public RotaService(IRotaRepository rotaRepository)
        {
            _rotaRepository = rotaRepository;
            
        }

        public bool CreateRota(Rota rota)
        {
            if (_rotaRepository.RotaExists(rota.Id))
                throw new Exception("Rota já existe");

            return _rotaRepository.CreateRota(rota);
        }

        public bool DeleteRota(Rota rota)
        {
            if (!_rotaRepository.RotaExists(rota.Id))
                throw new Exception("Rota não existe");

            return _rotaRepository.DeleteRota(rota);
        }

        public Rota GetRota(int id)
        {
            if (!_rotaRepository.RotaExists(id))
                throw new Exception("Rota não existe");

            return _rotaRepository.GetRota(id);
        }

        public ICollection<Rota> GetRotas()
        {
            return _rotaRepository.GetRotas();
        }

        public bool RotaExists(int rotaId)
        {
            return _rotaRepository.RotaExists(rotaId);
        }

        public bool UpdateRota(Rota rota)
        {
            if (!_rotaRepository.RotaExists(rota.Id))
                throw new Exception("Rota não existe");

            return _rotaRepository.UpdateRota(rota);
        }
    }
}
