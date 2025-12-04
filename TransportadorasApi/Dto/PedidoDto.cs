using TransportadorasApi.Model;

namespace TransportadorasApi.Dto
{
    public class PedidoCreateDto
    {
        public int EnderecoOrigemId { get; set; }
        public int EnderecoDestinoId { get; set; }
        public int ClienteId { get; set; }
        public int RotaId { get; set; }
        public List<int> ItensIds { get; set; }
    }

    public class PedidoUpdateDto
    {
        public int EnderecoOrigemId { get; set; }
        public int EnderecoDestinoId { get; set; }
        public int ClienteId { get; set; }
        public int RotaId { get; set; }
        public List<int> ItensIds { get; set; }
    }

}
