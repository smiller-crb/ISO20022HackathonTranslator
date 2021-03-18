namespace ISO20022HackathonTranslator.Models
{
    public class MtMessageApplicationHeader
    {
        public string InputOutput { get; set; }
        public string MessageType { get; set; }
        public string DestinationAddress { get; set; }
        public string Priority { get; set; }
        public string DeliveryMonitoring { get; set; }
        public string ObsolescencePriod { get; set; }
    }
}
