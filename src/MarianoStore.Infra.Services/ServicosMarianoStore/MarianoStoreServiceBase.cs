using MarianoStore.Core.Settings;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace MarianoStore.Infra.Services.ServicosMarianoStore
{
    public abstract class MarianoStoreServiceBase
    {
        private readonly DependenciesMarianoStoreServiceBase _dependencies;

        public MarianoStoreServiceBase(
            DependenciesMarianoStoreServiceBase dependencies)
        {
            _dependencies = dependencies;
        }

        public async Task<TResponse> ExecuteRequestAsync<TResponse>(Contexts context, Func<HttpClient, Task<HttpResponseMessage>> executeAsync)
        {
            HttpClient httpClient = _dependencies.HttpClientFactory.CreateClient(name: context.ToString());
            HttpResponseMessage response = await executeAsync(httpClient);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<TResponse>();
        }
    }
}
