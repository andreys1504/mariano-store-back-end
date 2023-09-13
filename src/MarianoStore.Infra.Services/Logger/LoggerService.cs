using MarianoStore.Core.Infra.Services.Logger;
using System;
using System.Threading.Tasks;

namespace MarianoStore.Infra.Services.Logger
{
    public class LoggerService : ILoggerService
    {
        public Task LogErrorRegisterAsync(Exception exception, string message)
        {
            return Task.CompletedTask;
        }
    }
}
