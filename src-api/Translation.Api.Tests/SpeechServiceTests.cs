using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Translation.Api.Service;
using Xunit;

namespace Translation.Api.Tests
{
    public class SpeechServiceTests
    {
        private readonly Mock<IConfiguration> _mockConfig;
        private readonly Mock<ILogger<SpeechService>> _mockLogger;
        private readonly SpeechService _speechService;

        public SpeechServiceTests()
        {
            _mockConfig = new Mock<IConfiguration>();
            _mockLogger = new Mock<ILogger<SpeechService>>();
            _speechService = new SpeechService(_mockConfig.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task SpeechToTextAsync_ThrowsArgumentException_WhenFileIsNull()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _speechService.SpeechToTextAsync(null));
        }

        [Fact]
        public async Task TextToSpeechAsync_ThrowsArgumentException_WhenTextIsEmpty()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _speechService.TextToSpeechAsync(string.Empty));
        }

        [Fact]
        public async Task TranslateTextAsync_ThrowsArgumentException_WhenInputsAreInvalid()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _speechService.TranslateTextAsync(null, "fr"));
            await Assert.ThrowsAsync<ArgumentException>(() => _speechService.TranslateTextAsync("Hello", null));
        }

        [Fact]
        public async Task TranslateTextAsync_ReturnsTranslatedText_WhenValidInputs()
        {
            // Arrange
            _mockConfig.Setup(c => c["AzureTranslator:Key"]).Returns("dummy-key");
            _mockConfig.Setup(c => c["AzureTranslator:Endpoint"]).Returns("https://dummy-endpoint/");
            _mockConfig.Setup(c => c["AzureTranslator:Region"]).Returns("westus");

            // Act & Assert
            var ex = await Assert.ThrowsAsync<Exception>(() =>
                _speechService.TranslateTextAsync("Hello", "fr"));

            Assert.Contains("Translation API failed", ex.Message);
        }
    }

}
