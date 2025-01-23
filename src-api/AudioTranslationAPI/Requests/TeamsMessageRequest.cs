namespace AudioTranslationAPI.Requests
{
    public class TeamsMessageRequest
    {
        public string TeamId { get; set; }
        public string ChannelId { get; set; }
        public string Message { get; set; }
    }
}
