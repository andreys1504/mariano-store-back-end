using System.Threading.Tasks;

namespace MarianoStore.Core.Services.Email
{
    public interface IEmailService
    {
        Task EnviarAsync();
    }
}
