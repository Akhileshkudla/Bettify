using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Telegram.Bot;

namespace Infrastructure.Telegram;

public class TelegramService : ITelegramService
{
    IConfiguration _configuration;
    private readonly ILogger<TelegramService> _logger;
    readonly string _channelId;

    public TelegramBotClient telegramBotClient {get;}

    public TelegramService(IConfiguration configuration , ILogger<TelegramService> logger)
    {
        _configuration = configuration;
        _logger = logger;
        var botToken = _configuration.GetSection("Telegram:BotToken").Value;
        _channelId = _configuration.GetSection("Telegram:Channel").Value;
        telegramBotClient = new TelegramBotClient(botToken);
    }

    /// <summary>
    /// Send a message to a Telegram chat/channel
    /// </summary>
    /// <param name="msg">Message text</param>
    /// <param name="sendTo">Recepient</param>
    public async Task<bool> SendMessageAsync(string msg)
    {
        try
        {   
            //await telegramBotClient.SendTextMessageAsync(_channelId,  msg);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error" + ex);
            return false;
        }
        return true;
    }
}