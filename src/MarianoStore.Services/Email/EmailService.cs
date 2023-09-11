using MarianoStore.Core.Services.Email;
using System.Threading.Tasks;

namespace MarianoStore.Services.Email
{
    public class EmailService : IEmailService
    {
        public Task EnviarAsync()
        {
            return Task.CompletedTask;
        }
    }
}
