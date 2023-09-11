namespace MarianoStore.Catalogo.Api.AsyncOperationsOnCatalogo.Events
{
    public class QueuesSettings
    {
        //Default
        public const string EventsExchange = "catalogo__events_exchange";
        public const string EventsQueue = "catalogo__events_queue";
        public const string EventsRoutingKey = "*.*.*";
    }
}
