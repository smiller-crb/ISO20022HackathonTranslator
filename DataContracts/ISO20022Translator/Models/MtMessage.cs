namespace ISO20022HackathonTranslator.Models
{
    public class MtMessage
    {
        public MtMessageHeader Header { get; set; }
        public MtMessageApplicationHeader ApplicationHeader { get; set; }
        public MtMessageUserHeader UserHeader { get; set; }
        public MtMessageBody Body { get; set; }
        public MtMessageTrailer Trailer { get; set; }
    }
}
