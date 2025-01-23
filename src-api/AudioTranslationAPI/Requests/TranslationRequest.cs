namespace AudioTranslationAPI.Requests
{
    public class TranslationRequest
    {
        public string Text { get; set; }
        public string TargetLanguage { get; set; }
    }


    public class DetectedLanguage
    {
        public string Language { get; set; }
        public double Score { get; set; }
    }

    public class Translation
    {
        public string Text { get; set; }
        public string To { get; set; }
    }

    public class TranslationResult
    {
        public DetectedLanguage DetectedLanguage { get; set; }
        public List<Translation> Translations { get; set; }
    }
}
