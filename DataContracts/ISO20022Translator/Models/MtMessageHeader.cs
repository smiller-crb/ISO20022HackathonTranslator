namespace ISO20022HackathonTranslator.Models
{
    public class MtMessageHeader
    {
        public string AppId { get; set; }
        public string ServiceId { get; set; }
        public string LogicalTerminalAddress { get; set; }
        public string SessionNumber { get; set; }
        public string SequenceNumber { get; set; }
    }
}
