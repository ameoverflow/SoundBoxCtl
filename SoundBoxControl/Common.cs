using System.Diagnostics;

namespace SoundBoxControl;

public static class Common
{
    public static string RunCommandAndGetOutput(string command)
    {
        try
        {
            var escapedArgs = command.Replace("\"", "\\\"");
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{escapedArgs}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit(1000);
            return output;
        }
        catch
        {
            return string.Empty;
        }
    }
}