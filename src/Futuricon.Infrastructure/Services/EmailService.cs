using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Futuricon.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;

        public EmailService(IConfiguration configuration)
        {
            _config = configuration;
        }

        public async Task SendTGAsync(string msg)
        {
            var bot = new Telegram.Bot.TelegramBotClient(_config.GetSection("TelegramAPIToken").Value);
            await bot.SendTextMessageAsync("258995364", msg);
        }
    }
}
