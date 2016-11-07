using System.Collections.Generic;

namespace TextAnalyticsClientBot
{

    // Classes to store the input for the sentiment API call
    public class BatchInput
    {
        public List<DocumentInput> Documents { get; set; }
    }
    public class DocumentInput
    {
        public double Id { get; set; }
        public string Text { get; set; }
    }

    // Classes to store the result from the sentiment analysis
    public class BatchResult
    {
        public List<DocumentResult> Documents { get; set; }
    }
    public class DocumentResult
    {
        public double Score { get; set; }
        public string Id { get; set; }
    }

    public class KeyPhraseResult
    {
        public List<KeyPhraseDocumentResult> Documents { get; set; }
    }
    public class KeyPhraseDocumentResult
    {
        public string[] KeyPhrases { get; set; }
        public string Id { get; set; }
    }

}