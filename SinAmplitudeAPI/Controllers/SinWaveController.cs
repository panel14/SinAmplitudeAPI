using Microsoft.AspNetCore.Mvc;
using SinAmplitudeAPI.Utils;

namespace SinAmplitudeAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SinWaveController : ControllerBase
    {
        private static readonly string FontPath = Path.Combine(Environment.CurrentDirectory + @"\Font\Orbitron.ttf");

        [HttpPost(Name = "GetSineWave")]
        public IActionResult GetSineWave(SineWaveModel model)
        {
            Image image = new Image<Rgba32>(900, 660);
            Drawer.DrawGrid(ref image, model.PeriodCount, (float)model.Amplitude, FontPath);
            Drawer.DrawSinWave(ref image, model);

            var stream = new MemoryStream();
            image.SaveAsJpeg(stream);

            return File(stream.ToArray(), "image/jpeg");
        }
    }
}