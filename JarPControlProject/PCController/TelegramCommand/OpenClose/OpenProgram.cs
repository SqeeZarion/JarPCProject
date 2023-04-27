using System.Diagnostics;
using JarPControlProject.Database;
using JarPControlProject.PCController.Command;
using Microsoft.Win32;

public class OpenProgram : ComInterfaceOC
{
    private CommandResult<String> result;
    private ProgramProcess pcControl;

    private readonly RegistryKey key =
        Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths");

    public OpenProgram()
    {
        pcControl = new ProgramProcess();
    }

    public CommandResult<String> Execute(String programName)
    {
        // Check if the program is already open
        if (!pcControl.SearchPrograms(programName))
        {
            try
            {
                string path = @"D:\Program Files (x86)\Telegram Desktop\";
                string?[] files =
                    Directory.GetFiles(path, programName + ".exe",
                        SearchOption.AllDirectories); // пошук всіх відповідних файлів
                string? fullPath = (files.Length > 0) ? files[0] : null;
                
                if (!string.IsNullOrEmpty(fullPath))
                {
                    // Open the program

                    Process.Start(fullPath);
                    ProgramProcess.Name.Add(Process.GetProcesses().Length - 1, programName);

                    result = new CommandResult<String>("Succeed!", "Program" + programName + "opened successfully.!",
                        true);
                }

                return null!;
            }
            catch (Exception e)
            {
                result = new CommandResult<String>("Failed!", "Failed to open program. Error: " + e.Message, false);
            }
        }
        else
            result = new CommandResult<String>("Failed!", "Program " + programName + " is already open.", false);

        return result;
    }
}