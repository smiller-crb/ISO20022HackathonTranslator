namespace ISO20022HackathonTranslator.Models
{
    public class MtMessageTrailer
    {
        public string Checksum { get; set; }
        public string TestTrainingMessage { get; set; }
        public string PossibleDuplicateEmission { get; set; }
        public string DelayedMessage { get; set; }
        public string MessageReference { get; set; }
        public string PossibleDuplicateMessage { get; set; }
        public string SystemOriginatedMessage { get; set; }
    }
}
