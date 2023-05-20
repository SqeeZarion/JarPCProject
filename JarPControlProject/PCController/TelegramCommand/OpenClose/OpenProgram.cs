using System.Diagnostics;
using JarPControlProject.Database;
using JarPControlProject.PCController.Command;
using Microsoft.Win32;

public class OpenProgram : ComInterfaceOC
{
    private CommandResult<String> result;
    private ProgramProcess pcControl;
    
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
                if (!string.IsNullOrEmpty(programName))
                {
                    string[] files = Directory.GetFiles(@"D:\Program Files (x86)", programName+".exe", SearchOption.AllDirectories);

                    if (files.Length > 0)
                    {
                        Process.Start(files[0]);
                        ProgramProcess.Name.Add(Process.GetProcesses().Length - 1, programName);

                        result = new CommandResult<String>("Succeed!", "Program" + programName + "opened successfully.!", true);
                    }
                }

                return null!;
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed!" + e.Message, false);
            }
        }
        else
            result = new CommandResult<String>("Failed!", "Program " + programName + " is already open.", false);

        return result;
    }
}