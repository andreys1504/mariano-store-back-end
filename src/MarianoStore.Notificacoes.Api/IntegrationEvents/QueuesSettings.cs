namespace MarianoStore.Notificacoes.Api.IntegrationEvents
{
    public class QueuesSettings
    {
        public class PAGAMENTO
        {
            public const string PagamentoEventsExchange = "pagamento__events_exchange";

            public class PagamentoEvents
            {
                public const string Queue = "p:pagamento_c:notificacoes__pagamento_events_queue";
                public const string RoutingKey = "pagamento.pagamento.*";
            }
        }

        public class PEDIDOS
        {
            public const string PedidoEventsExchange = "pedidos__events_exchange";

            public class PedidoEvents
            {
                public const string Queue = "p:pedidos_c:notificacoes__pedido_events_queue";
                public const string RoutingKey = "pedidos.pedido.*";
            }
        }
    }
}
