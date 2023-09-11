namespace MarianoStore.Catalogo.Api.IntegrationEvents
{
    public class IntegrationEventsQueuesSettings
    {
        public const string EventsExchange = "catalogo__events_exchange";
        public const string EventsQueue = "catalogo__events_queue";
        public const string EventsRoutingKey = "*.*.*";

        public class PEDIDOS
        {
            public const string PedidoEventsExchange = "pedidos__events_exchange";

            public class PedidoEvents
            {
                public const string Queue = "p:pedidos_c:catalogo__pedido_events_queue";
                public const string RoutingKey = "pedidos.pedido.*";
            }
        }
    }
}
