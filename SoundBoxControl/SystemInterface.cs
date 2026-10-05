namespace SoundBoxControl;

public static class SystemInterface
{
    public static bool GetServiceStatus(string service)
    {
        string output = Common.RunCommandAndGetOutput($"systemctl --machine=void@.host --user is-active {service}");
        bool isRunning = output.Trim().Equals("active", StringComparison.OrdinalIgnoreCase);
        return isRunning;
    }
}