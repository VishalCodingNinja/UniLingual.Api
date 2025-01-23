using Microsoft.AspNetCore.Http;

namespace Translation.Api.Service
{
    public interface ISpeechService
    {
        Task<string> SpeechToTextAsync(IFormFile audioFile);
        Task<byte[]> TextToSpeechAsync(string text);
        Task<string> TranslateTextAsync(string text, string toLanguage);
    }

}