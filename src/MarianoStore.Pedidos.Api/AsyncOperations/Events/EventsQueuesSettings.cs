namespace MarianoStore.Pedidos.Api.AsyncOperations.Events
{
    public class EventsQueuesSettings
    {
        public const string EventsExchange = "pedidos__events_exchange";
        public const string EventsQueue = "pedidos__events_queue";
        public const string EventsRoutingKey = "*.*.*";

        public class PedidoRealizadoSucessoEvent
        {
            public const string RoutingKey = "pedidos.pedido.pedido_realizado_sucesso";
        }
    }
}
