namespace TransportadorasApi.Model
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Number { get; set; } 
        

        public Endereco endereco { get; set; }


    }
}
