using MarianoStore.Core.Infra.Services.Email;
using System.Threading.Tasks;

namespace MarianoStore.Infra.Services.Email
{
    public class EmailService : IEmailService
    {
        public Task EnviarAsync()
        {
            return Task.CompletedTask;
        }
    }
}
