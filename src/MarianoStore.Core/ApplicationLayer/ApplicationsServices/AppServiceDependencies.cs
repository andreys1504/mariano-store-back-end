using MarianoStore.Core.Mediator;

namespace MarianoStore.Core.ApplicationLayer.ApplicationsServices
{
    public class AppServiceDependencies
    {
        public AppServiceDependencies(
            IMediatorHandler mediatorHandler)
        {
            MediatorHandler = mediatorHandler;
        }

        public IMediatorHandler MediatorHandler { get; }
    }
}
