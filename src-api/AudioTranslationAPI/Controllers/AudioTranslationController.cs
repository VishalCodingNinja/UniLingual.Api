using Microsoft.AspNetCore.Mvc;
using Translation.Api.Service;

namespace AudioTranslationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AudioTranslationController : ControllerBase
    {
        private readonly ISpeechService _speechService;
        private readonly ILogger<AudioTranslationController> _logger;

        public AudioTranslationController(ISpeechService speechService, ILogger<AudioTranslationController> logger)
        {
            _speechService = speechService;
            _logger = logger;
        }

        [HttpPost("speech-to-text")]
        public async Task<IActionResult> SpeechToText(IFormFile audioFile)
        {
            try
            {
                var result = await _speechService.SpeechToTextAsync(audioFile);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SpeechToText");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("text-to-speech")]
        public async Task<IActionResult> TextToSpeech([FromForm] string text)
        {
            try
            {
                var audioBytes = await _speechService.TextToSpeechAsync(text);
                return File(audioBytes, "audio/wav", "output.wav");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in TextToSpeech");
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("translate-text")]
        public async Task<IActionResult> TranslateText([FromQuery] string text, [FromQuery] string toLanguage)
        {
            try
            {
                var result = await _speechService.TranslateTextAsync(text, toLanguage);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in TranslateText");
                return StatusCode(500, ex.Message);
            }
        }
    }

}
