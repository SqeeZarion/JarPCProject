// See https://aka.ms/new-console-template for more information

using System.Data.SqlClient;
using JarPControlProject.PCController.Command;
using JarPControlProject.Telegram;
using System.Diagnostics;
using JarPControlProject.Database;
using JarPControlProject.PCController.Enity;


class Program
{
    static void Main(string[] args)
    {
         InvokerOpenClose doer;
         PCControl pccontrol = new PCControl("SqeeZes");
         TelegramBot bot = new TelegramBot("5697150776:AAFZilQrfm3LEOIUfNl4yDcKgWbnRBfCx44");
        
        //OpenProgram method
        
        // ComInterfaceOC open = new OpenProgram();
        //
        // doer = new InvokerOpenClose(open);
        //
        // doer.doSmth();
        //
        // //CloseProgram method
        //
        // ComInterfaceOC close = new CloseProgram();
        //
        // doer = new InvokerOpenClose(close);
        
        // doer.doSmth();
        
        while (true)
        {
            try
            {
                bot.GetMessage().Wait();
            }
            catch (Exception e)
            {
                Console.WriteLine("Eror:" + e);
                throw;
            }
        }
        
    }
}