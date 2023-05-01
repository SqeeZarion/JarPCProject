namespace JarPControlProject.PCController.Command;

public interface ComInterfaceOC
{
    public CommandResult<String> Execute(String programName);
}