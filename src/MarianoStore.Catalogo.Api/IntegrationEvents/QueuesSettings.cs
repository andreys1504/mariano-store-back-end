namespace MarianoStore.Catalogo.Api.IntegrationEvents
{
    public class QueuesSettings
    {
        public class PAGAMENTO
        {
            public const string PagamentoEventsExchange = "pagamento__events_exchange";

            public class PagamentoEvents
            {
                public const string Queue = "p:pagamento_c:catalogo__pagamento_events_queue";
                public const string RoutingKey = "pagamento.pagamento.*";
            }
        }
    }
}
