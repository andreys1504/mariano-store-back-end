using System.Collections.Generic;

namespace MarianoStore.Core.Settings.ServicosMarianoStore
{
    public class ServicosMarianoStoreSettings
    {
        public IList<Servico_ServicosMarianoStoreSettings> Servicos { get; set; } = new List<Servico_ServicosMarianoStoreSettings>();
    }
}
