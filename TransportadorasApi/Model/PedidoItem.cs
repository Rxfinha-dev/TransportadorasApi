namespace TransportadorasApi.Model
{
    public class PedidoItem
    {
        public int PedidoId { get; set; }
        public int ItemId { get; set; }

        public  Pedido Pedido { get; set; }

        public Item Item { get; set; }

    }
}
