using System.Diagnostics;

namespace JarPControlProject.Database;

public class ProgramProcess
{
    private static Dictionary<int, string> name;

    public static Dictionary<int, string> Name
    {
        get => name;
        set => name = value ?? throw new ArgumentNullException(nameof(value));
    }

    public ProgramProcess()
    {
        Name = new Dictionary<int, string>();
    }

    public static Dictionary<int, string> GetRunningPrograms()
    {
        try
        {
            Process[] processes = Process.GetProcesses();
            Name = new Dictionary<int, string>();

            for (int i = 0; i < processes.Length; i++)
                name.Add(i, processes[i].ProcessName);
        }
        catch (Exception e)
        {
            Console.WriteLine("Eror" + e.Message);
            throw;
        }

        return name;
    }

    public bool SearchPrograms(String programName)
    {
        name = GetRunningPrograms();
        if (name.ContainsValue(programName))
            return true;

        return false;
    }

    public static Process GetProcessByName(string processName)
    {
        Process[] processes = Process.GetProcessesByName(processName);
        if (processes.Length > 0)
            return processes[0];

        return null;
    }
}