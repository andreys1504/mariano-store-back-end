namespace MarianoStore.Pedidos.Api.AsyncOperationsOnPedidos.Commands
{
    public class QueuesSettings
    {
        //Default
        public const string CommandsExchange = "pedidos__commands_exchange";
        public const string CommandsQueue = "pedidos__commands_queue";
        public const string CommandsRoutingKey = "pedidos_commands_routingkey";
    }
}
