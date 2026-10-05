using System.Globalization;
using System.Text.RegularExpressions;

namespace SoundBoxControl;

public static class PipewireInterface
{
    public static void SetVolume(int volume)
    {
        double targetVolume = Math.Clamp(volume / 100.0, 0.0, 1.5);
        Common.RunCommandAndGetOutput($"wpctl set-volume @DEFAULT_AUDIO_SINK@ {targetVolume:0.00}");
    }

    public static void SetMuted(bool isMuted)
    {
        string muteArg = isMuted ? "1" : "0";
        Common.RunCommandAndGetOutput($"wpctl set-mute @DEFAULT_AUDIO_SINK@ {muteArg}");
    }
    
    public static (int Volume, bool IsMuted) GetAudioState()
    {
        string output = Common.RunCommandAndGetOutput("wpctl get-volume @DEFAULT_AUDIO_SINK@");
        
        if (string.IsNullOrWhiteSpace(output))
        {
            return (80, false); // fallback default
        }

        int volume = 80;
        bool isMuted = output.Contains("[MUTED]", StringComparison.OrdinalIgnoreCase);
        
        var match = Regex.Match(output, @"([0-9]*\.[0-9]+)");
        if (match.Success && double.TryParse(match.Value, NumberStyles.Any, CultureInfo.InvariantCulture, out double parsedVol))
        {
            volume = (int)Math.Round(parsedVol * 100);
        }

        return (Math.Clamp(volume, 0, 150), isMuted);
    }
}