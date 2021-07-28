using System.Threading.Tasks;

namespace Futuricon.Infrastructure.Services
{
    public interface IEmailService
    {
        Task SendTGAsync(string msg);
    }
}
