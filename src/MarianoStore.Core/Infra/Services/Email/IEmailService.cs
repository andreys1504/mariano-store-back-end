using System.Threading.Tasks;

namespace MarianoStore.Core.Infra.Services.Email
{
    public interface IEmailService
    {
        Task EnviarAsync();
    }
}
