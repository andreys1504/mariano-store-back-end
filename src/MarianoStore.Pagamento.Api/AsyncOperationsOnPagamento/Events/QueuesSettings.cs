namespace MarianoStore.Pagamento.Api.AsyncOperationsOnPagamento.Events
{
    public class QueuesSettings
    {
        //Default
        public const string EventsExchange = "pagamento__events_exchange";
        public const string EventsQueue = "pagamento__events_queue";
        public const string EventsRoutingKey = "*.*.*";

        public class PagamentoRealizadoSucessoEvent
        {
            public const string RoutingKey = "pagamento.pagamento.pagamento_realizado_sucesso";
        }
    }
}
