using AudioTranslationAPI.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System.Text;
using Translation.Api.Service;
using Xunit;
namespace Translation.Api.Tests
{
    public class AudioTranslationControllerTests
    {
        private readonly Mock<ISpeechService> _mockSpeechService;
        private readonly Mock<ILogger<AudioTranslationController>> _mockLogger;
        private readonly AudioTranslationController _controller;

        public AudioTranslationControllerTests()
        {
            _mockSpeechService = new Mock<ISpeechService>();
            _mockLogger = new Mock<ILogger<AudioTranslationController>>();
            _controller = new AudioTranslationController(_mockSpeechService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task SpeechToText_ReturnsOk_WhenValidAudioFile()
        {
            // Arrange
            var mockFile = CreateMockFormFile("test.wav", "audio/wav", "dummy content");
            _mockSpeechService.Setup(s => s.SpeechToTextAsync(It.IsAny<IFormFile>()))
                              .ReturnsAsync("Transcribed text");

            // Act
            var result = await _controller.SpeechToText(mockFile);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Transcribed text", okResult.Value);
        }

        [Fact]
        public async Task TextToSpeech_ReturnsFile_WhenValidText()
        {
            // Arrange
            var mockAudioBytes = Encoding.UTF8.GetBytes("audio content");
            _mockSpeechService.Setup(s => s.TextToSpeechAsync(It.IsAny<string>()))
                              .ReturnsAsync(mockAudioBytes);

            // Act
            var result = await _controller.TextToSpeech("Hello, world!");

            // Assert
            var fileResult = Assert.IsType<FileContentResult>(result);
            Assert.Equal("audio/wav", fileResult.ContentType);
            Assert.Equal("output.wav", fileResult.FileDownloadName);
            Assert.Equal(mockAudioBytes, fileResult.FileContents);
        }

        [Fact]
        public async Task TranslateText_ReturnsOk_WhenValidTextAndLanguage()
        {
            // Arrange
            _mockSpeechService.Setup(s => s.TranslateTextAsync("Hello", "fr"))
                              .ReturnsAsync("Bonjour");

            // Act
            var result = await _controller.TranslateText("Hello", "fr");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Bonjour", okResult.Value);
        }

        private IFormFile CreateMockFormFile(string fileName, string contentType, string content)
        {
            var fileMock = new Mock<IFormFile>();
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(content));
            fileMock.Setup(f => f.OpenReadStream()).Returns(stream);
            fileMock.Setup(f => f.FileName).Returns(fileName);
            fileMock.Setup(f => f.ContentType).Returns(contentType);
            return fileMock.Object;
        }
    }


}