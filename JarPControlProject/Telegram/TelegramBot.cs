using Telegram.Bot;
using Telegram.Bot.Types;

namespace JarPControlProject.Telegram;

public class TelegramBot
{
    // A field containing the bot's token to access the Telegram API
    private static String token;

    // Field containing the ID of the chat from which the commands will be received
    private long chatId;
    
    private TelegramBotClient botClient = null!;

    private Message message;


    public long ChatId
    {
        get => chatId;
        set => chatId = value;
    }

    public TelegramBotClient BotClient
    {
        get => botClient;
        set => botClient = value ?? throw new ArgumentNullException(nameof(value));
    }

    public  Message Message
    {
        get => message;
        set => message = value ?? throw new ArgumentNullException(nameof(value));
    }

    public static string Token
    {
        get => token;
        set => token = value ?? throw new ArgumentNullException(nameof(value));
    }
    
    public TelegramBot(String token)
    {
        TelegramBot.token = token;
    }

    public async Task GetMessage() // authorization method
    {
        BotClient = new TelegramBotClient(Token);
        int offset = 0;
        int timeOut = 0;

        try
        {
            await BotClient.SetWebhookAsync(""); //The await keyword in
            //asynchronous programming indicates
            //that the current function is waiting for the result of another asynchronous operation.

            while (true)
            {
                var updates = await BotClient.GetUpdatesAsync(offset, timeOut);

                foreach (var update in updates)
                {
                     message = update.Message; //we receive a message

                    if (message?.Text == "My message")
                    {
                        Console.WriteLine("Message: " + message);
                        await BotClient.SendTextMessageAsync(message.Chat.Id, "good job" + message.Chat.Username);
                    }

                    offset = update.Id + 1;
                }
            }
        }
        catch (Exception e) //логери потім добав або як у Макса
        {
            Console.WriteLine("Error" + e);
            throw;
        }
    }
}