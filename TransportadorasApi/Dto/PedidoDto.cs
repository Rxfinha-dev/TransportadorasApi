using TransportadorasApi.Model;

namespace TransportadorasApi.Dto
{
    public class PedidoDto
    {
        public int Id { get; set; }
        public int EnderecoOrigemId { get; set; }
        public int EnderecoDestinoId { get; set; }
        public int ClienteId { get; set; }
        public int RotaId { get; set; }
        public List<int> ItensIds { get; set; }
    }

 }
