using MarianoStore.Core.Services.Logger;
using System;
using System.Threading.Tasks;

namespace MarianoStore.Services.Logger
{
    public class LoggerService : ILoggerService
    {
        public Task LogErrorRegisterAsync(Exception exception, string message)
        {
            return Task.CompletedTask;
        }
    }
}
