using System.Net.Http;

namespace MarianoStore.Infra.Services.ServicosMarianoStore
{
    public class DependenciesMarianoStoreServiceBase
    {
        public DependenciesMarianoStoreServiceBase(
            IHttpClientFactory httpClientFactory)
        {
            HttpClientFactory = httpClientFactory;
        }

        public IHttpClientFactory HttpClientFactory { get; }
    }
}
