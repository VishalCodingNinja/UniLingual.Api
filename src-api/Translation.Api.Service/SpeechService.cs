using Microsoft.AspNetCore.Http;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Translation.Api.Service
{

    public class SpeechService : ISpeechService
    {
        private readonly IConfiguration _config;
        private readonly ILogger<SpeechService> _logger;

        public SpeechService(IConfiguration config, ILogger<SpeechService> logger)
        {
            _config = config;
            _logger = logger;
        }

        public async Task<string> SpeechToTextAsync(IFormFile audioFile)
        {
            try
            {
                if (audioFile == null) throw new ArgumentException("Invalid audio file.");

                var subscriptionKey = _config["AzureAI:SubscriptionKey"];
                var region = _config["AzureAI:Region"];
                var endpoint = $"https://{region}.stt.speech.azure.com/speech/recognition/conversation/cognitiveservices/v1";

                using var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);

                using var audioContent = new StreamContent(audioFile.OpenReadStream());
                audioContent.Headers.ContentType = new MediaTypeHeaderValue("audio/wav");

                var response = await client.PostAsync(endpoint, audioContent);
                if (!response.IsSuccessStatusCode)
                    throw new Exception($"Speech-to-text API failed: {response.StatusCode}");

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in SpeechToTextAsync");
                throw;
            }
        }

        public async Task<byte[]> TextToSpeechAsync(string text)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(text))
                    throw new ArgumentException("Text cannot be empty.");

                var subscriptionKey = _config["SpeechService:SubscriptionKey"];
                var region = _config["SpeechService:Region"];
                var config = SpeechConfig.FromSubscription(subscriptionKey, region);
                config.SpeechSynthesisVoiceName = "en-US-JennyNeural";

                var tempPath = Path.GetTempFileName();
                using (var audioOutput = AudioConfig.FromWavFileOutput(tempPath))
                using (var synthesizer = new SpeechSynthesizer(config, audioOutput))
                {
                    var result = await synthesizer.SpeakTextAsync(text);
                    if (result.Reason != ResultReason.SynthesizingAudioCompleted)
                        throw new Exception("Text-to-speech synthesis failed.");
                }

                var audioBytes = await File.ReadAllBytesAsync(tempPath);
                File.Delete(tempPath);
                return audioBytes;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in TextToSpeechAsync");
                throw;
            }
        }

        public async Task<string> TranslateTextAsync(string text, string toLanguage)
        {
            try
            {
                if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(toLanguage))
                    throw new ArgumentException("Text and target language must be provided.");

                var subscriptionKey = _config["AzureTranslator:Key"];
                var endpoint = _config["AzureTranslator:Endpoint"];
                var region = _config["AzureTranslator:Region"];
                var url = $"{endpoint}translate?api-version=3.0&to={toLanguage}";

                var requestBody = new[] { new { Text = text } };
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Region", region);

                var response = await client.PostAsJsonAsync(url, requestBody);
                if (!response.IsSuccessStatusCode)
                    throw new Exception($"Translation API failed: {response.StatusCode}");

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in TranslateTextAsync");
                throw;
            }
        }
    }

}
