namespace MarianoStore.Pedidos.Api.AsyncOperationsOnPedidos.Events
{
    public class QueuesSettings
    {
        //Default
        public const string EventsExchange = "pedidos__events_exchange";
        public const string EventsQueue = "pedidos__events_queue";
        public const string EventsRoutingKey = "*.*.*";

        public class PedidoRealizadoSucessoEvent
        {
            public const string RoutingKey = "pedidos.pedido.pedido_realizado_sucesso";
        }
    }
}
