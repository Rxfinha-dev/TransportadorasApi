using TransportadorasApi.Model;

namespace TransportadorasApi.Dto
{
    public class ClienteDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Number { get; set; }


        public Endereco endereco { get; set; }
    }
}
