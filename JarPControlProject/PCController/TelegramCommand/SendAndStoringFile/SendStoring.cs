using JarPControlProject.Telegram;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace JarPControlProject.PCController.Command.SendAndStoringFile;

public class SendStoring
{
    private string fileid;
    private TelegramBotClient file;

    public string Fileid
    {
        get => fileid;
        set => fileid = value ?? throw new ArgumentNullException(nameof(value));
    }

    public TelegramBotClient File
    {
        get => file;
        set => file = value ?? throw new ArgumentNullException(nameof(value));
    }

   
    public SendStoring(string fileid, TelegramBotClient file)
    {
        this.fileid = fileid;
        this.file = file;
    }

    static async Task SaveFile(TelegramBotClient botClient, Message message)
    {
        string fileid = message.Document.FileId;
        var file = await botClient.GetFileAsync(fileid); //get the file woth ID
        
    }
    
}