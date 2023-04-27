using System.Diagnostics;
using JarPControlProject.Database;
using JarPControlProject.Telegram;


namespace JarPControlProject.PCController.Command;

public class CloseProgram :ComInterfaceOC
{
    private ProgramProcess pcControl;
    private CommandResult<String> result;

    public CloseProgram()
    {
        pcControl = new ProgramProcess();
    }

    public CommandResult<String> Execute(String programName)
    {
        if (pcControl.SearchPrograms(programName))
        {
            try
            {
                // Close the program
                // You can use Process.Kill() or Process.CloseMainWindow() methods
                //Check program

                Process process = ProgramProcess.GetProcessByName(programName);
                process.Kill();

                ProgramProcess.Name.Remove(ProgramProcess.Name.FirstOrDefault(x => x.Value == programName).Key);

                result = new CommandResult<String>("Succeed!", "Program " + programName + " closed successfully.",
                    true);
            }
            catch (Exception e)
            {
                result = new CommandResult<String>("Failed!", "Failed to close program. Error: " + e.Message, false);
            }
        }
        else
            result = new CommandResult<String>("Failed!", "Program " + programName + " is not open.", false);

        return result;
    }
}