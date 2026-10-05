using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SoundBoxControl.Pages;

public class IndexModel : PageModel
{
    public int Volume { get; set; } = 80;
    
    public bool IsMuted { get; set; }
    
    public Dictionary<string, bool> ServiceStatuses { get; set; } = new();
    
    public void OnGet()
    {
        var state = PipewireInterface.GetAudioState();
        Volume = state.Volume;
        IsMuted = state.IsMuted;
        
        string[] services = { "pipewire", "wireplumber", "librespot", "cringebox" };
        
        foreach (var svc in services)
        {
            ServiceStatuses[svc] = SystemInterface.GetServiceStatus(svc);
        }
    }
}