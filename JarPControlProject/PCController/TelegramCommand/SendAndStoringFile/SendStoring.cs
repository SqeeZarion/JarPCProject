using JarPControlProject.Telegram;
using Telegram.Bot;
using Telegram.Bot.Types;
using File = Telegram.Bot.Types.File;



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

    public static async Task SaveFile(TelegramBotClient botClient, Message message)
    {
        // Get the file ID and download the file
        string fileId = message.Document.FileId;
        var file = await botClient.GetFileAsync(fileId);
        var stream = new MemoryStream();
        await botClient.DownloadFileAsync(file.FilePath, stream);

        // Save the file to disk
        string fileName = message.Document.FileName;

        if (fileName != null)
        {
            string folderName = String.Format("{0}", message.Chat.Username);
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), folderName);
            Directory.CreateDirectory(path);

            string filePath = Path.Combine(path, fileName);
            try
            {
                using (var output = new FileStream(filePath, FileMode.Create))
                {   
                    stream.Position = 0;
                    await stream.CopyToAsync(output);
                }
                Console.WriteLine($"File {fileName} saved to {filePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving file: {ex.Message}");
            }
        }
    }

    public static async Task SendFile(TelegramBotClient botClient, Message message)
    {
        string folderName = String.Format("{0}", message.Chat.Username);
        string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), folderName);

        try
        {
            var info = new DirectoryInfo(path);
            FileInfo[] listFile = info.GetFiles();

            foreach (var file in listFile)
            {
                var fileStream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                var fileToSend = new InputFile(fileStream, file.Name);

                await botClient.SendDocumentAsync(message.Chat.Id, fileToSend);
                fileStream.Close(); // Закриваємо потік, щоб звільнити файл
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending file: {ex.Message}");
        }
    }
    
}