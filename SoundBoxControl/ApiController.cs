using Microsoft.AspNetCore.Mvc;

namespace SoundBoxControl;

[Route("api/audio")]
[ApiController]
public class AudioApiController : ControllerBase
{
    [HttpPost("volume")]
    public IActionResult SetVolume([FromQuery] int value)
    {
        PipewireInterface.SetVolume(value);
        var state = PipewireInterface.GetAudioState();
        return Ok(new { state.Volume });
    }
    
    [HttpPost("mute")]
    public IActionResult SetMuted([FromQuery] bool value)
    {
        PipewireInterface.SetMuted(value);
        var state = PipewireInterface.GetAudioState();
        return Ok(new { state.IsMuted });
    }
    
    [HttpGet("volume")]
    public IActionResult GetVolume()
    {
        var state = PipewireInterface.GetAudioState();
        return Ok(new { state.Volume });
    }
    
    [HttpGet("mute")]
    public IActionResult GetMuted()
    {
        var state = PipewireInterface.GetAudioState();
        return Ok(new { state.IsMuted });
    }
}

[Route("api/system")]
[ApiController]
public class SystemApiController : ControllerBase
{
    [HttpGet("status")]
    public IActionResult GetStatus([FromQuery] string service)
    {
        var isRunning = SystemInterface.GetServiceStatus(service);
        return Ok(new { isRunning });
    }
}