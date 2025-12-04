namespace TransportadorasApi.Dto
{
    public class EnderecoDto
    {
        public int Id { get; set; }
        public string Rua { get; set; }

        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public string Numero { get; set; }
        public string cep { get; set; }
        public string Estado { get; set; }

    }
}
