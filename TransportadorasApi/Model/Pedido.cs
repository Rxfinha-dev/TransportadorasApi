namespace TransportadorasApi.Model
{
    public class Pedido
    {
        public int Id { get; set; }
        public  int EnderecoOrigemId { get; set; }
        public  int EnderecoDestinoId { get; set; }
        public Rota Rota { get; set; }
        public Cliente Cliente { get; set; }
        public ICollection<PedidoItem> PedidoItems { get; set; }

        
    }
}
