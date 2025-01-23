namespace AudioTranslation.Api.Logger
{
    public class CustomLogger : ICustomLogger
    {
        private readonly ILogger _logger;

        public CustomLogger(ILogger<CustomLogger> logger)
        {
            _logger = logger;
        }

        public void LogInfo(string message) => _logger.LogInformation(message);
        public void LogError(string message) => _logger.LogError(message);
        public void LogDebug(string message) => _logger.LogDebug(message);
    }

    public interface ICustomLogger
    {
        void LogInfo(string message);
        void LogError(string message);
        void LogDebug(string message);
    }
}
