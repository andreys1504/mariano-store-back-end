using System;
using System.Threading.Tasks;

namespace MarianoStore.Core.Services.Logger
{
    public interface ILoggerService
    {
        Task LogErrorRegisterAsync(Exception exception, string message);
    }
}
