using TransportadorasApi.Model;

namespace TransportadorasApi.Dto
{
    public class DepositoDto
    {
        public int Id { get; set; }

        public string Nome { get; set; }
        public Endereco Localizacao { get; set; }
    }
}
