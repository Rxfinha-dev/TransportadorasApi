namespace TransportadorasApi.Model
{
    public class Item
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Description { get; set; }
        public ICollection<PedidoItem> PedidoItems { get; set; }

    }
}
