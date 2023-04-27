namespace JarPControlProject.PCController.Command;

public class InvokerOpenClose
{
    private ComInterfaceOC command;


    public InvokerOpenClose(ComInterfaceOC command)
    {
        this.command = command;
    }

    public void doSmth()
    {
        try
        {
            command.Execute(@"Telegram");
        }

        catch (IOException e)
        {
            if (e.Source != null)
                Console.WriteLine("IOException source: {0}", e.Source);
            throw;
        }
    }
}