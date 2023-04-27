using JarPControlProject.Database;

namespace JarPControlProject.PCController.Enity;

public class PCControl
{
    // A field containing the name of the computer
    private String computerName;

    // A field indicating whether music is playing
    private Boolean isMusicPlaying;

    // A field containing the current sound volume
    private int volume;

    private ProgramProcess openProgramsProcess;


    public int Volume
    {
        get { return volume; }
        set { volume = value; }
    }

    private Boolean IsMusicPlaying
    {
        get { return isMusicPlaying; }
        set { isMusicPlaying = value; }
    }

    public PCControl(String computerName)
    {
        this.computerName = computerName;
        this.isMusicPlaying = false;
        this.volume = 50;
        openProgramsProcess = new();
    }
}