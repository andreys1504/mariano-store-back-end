namespace MarianoStore.Pagamento.Api.IntegrationEvents
{
    public class IntegrationEventsQueuesSettings
    {
        public class PEDIDOS
        {
            public const string PedidoEventsExchange = "pedidos__events_exchange";

            public class PedidoEvents
            {
                public const string Queue = "p:pedidos_c:pagamento__pedido_events_queue";
                public const string RoutingKey = "pedidos.pedido.*";
            }
        }
    }
}
