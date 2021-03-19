namespace ISO20022HackathonTranslator.Models
{
    public class MtMessageBody
    {
        public string SenderReference { get; set; }
        public string TimeIndication { get; set; }
        public BankOperationCode? BankOperationCode { get; set; }
        public string InstructionCode { get; set; }
        public string TransactionTypeCode { get; set; }
        public string ValueDate { get; set; }
        public string SettledCurrency { get; set; }
        public string InterbankSettledAmount { get; set; }
        public string InstructedCurrency { get; set; }
        public string InstructedAmount { get; set; }
        public string ExchangeRate { get; set; }
        public MtCustomer OrderingCustomer { get; set; }
        public string SendingInstitution { get; set; }
        public string OrderingInstitution { get; set; }
        public string SendersCorrespondent { get; set; }
        public string ReceiversCorrespondent { get; set; }
        public string ThirdReimbursementInstitution { get; set; }
        public string IntermediaryInstitution { get; set; }
        public string AccountWithInstitution { get; set; }
        public MtCustomer BeneficiaryCustomer { get; set; }
        public string RemittanceInformation { get; set; }
        public string DetailsOfCharges { get; set; }
        public string SendersCharges { get; set; }
        public string ReceiversCharges { get; set; }
        public string SenderToReceiverInformation { get; set; }
        public string RegulatoryReporting { get; set; }
    }

    public class MtCustomer
    {
        public string PartyIndentifier { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Town { get; set; }
        public string Country { get; set; }
    }

    public enum BankOperationCode
    {
        NormalCreditTransfer,
        TestMessage,
        SWIFTPay,
        Priority,
        Standard
    }
}
