namespace ISO20022HackathonTranslator.Models
{
    public class MtMessageUserHeader
    {
        public string ServiceIdentifier { get; set; }
        public string BankingPriority { get; set; }
        public string MessageUserReference { get; set; }
        public string ValidationFlag { get; set; }
        public string ServiceTypeIdentifier { get; set; }
        public string EndToEndTransactionReference { get; set; }
        public string SanctionsScreeningInformation { get; set; }
        public string PaymentControlsInformation { get; set; }
    }
}
