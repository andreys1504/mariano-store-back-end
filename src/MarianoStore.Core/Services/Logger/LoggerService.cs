using System;
using System.Threading.Tasks;

namespace MarianoStore.Core.Services.Logger
{
    public class LoggerService : ILoggerService
    {
        public Task LogErrorRegisterAsync(Exception exception, string message)
        {
            return Task.CompletedTask;
        }
    }
}
