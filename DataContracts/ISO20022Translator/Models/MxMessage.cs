using System;

namespace ISO20022HackathonTranslator.Models.Mx00800102
{
    public class Document
    {
        public Transaction FIToFICstmrCdtTrf { get; set; }
    }

    public class Transaction
    {
        public GroupHeader GrpHdr { get; set; }

        public CreditTransferTransactionInformation[] CdtTrfTxInf { get; set; }
    }

    public class GroupHeader
    {
        public string MsgId { get; set; }
        public string CreDtTm { get; set; }
        public int NbOfTxs { get; set; }
        public decimal TtlIntrBkSttlmAmt { get; set; }
        public string Ccy { get; set; }
        public string IntrBkSttlmDt { get; set; }

        public SettlementInformation SttlmInf { get; set; }

        public Agent InstgAgt { get; set; }

        public Agent InstdAgt { get; set; }

        //        public static implicit operator GroupHeader(GroupHeader v)
        //        {
        //           throw new NotImplementedException();
        //       }
    }

    public class CreditTransferTransactionInformation
    {
        public PaymentId PmtId { get; set; }
        public PaymentTypeInformation PmtTpInf { get; set; }
        public decimal IntrBkSttlmAmt { get; set; }
        public string ChrgBr { get; set; }
        public Party Dbtr { get; set; }
        public PartyAccount DbtrAcct { get; set; }
        public PartyAgent DbtrAgt { get; set; }
        public PartyAgent CdtrAgt { get; set; }
        public Party Cdtr { get; set; }
        public PartyAccount CdtrAcct { get; set; }
        public RemittanceInformation RmtInf { get; set; }
    }

    public class RemittanceInformation
    {
        public string Ustrd { get; set; }
    }

    public class PartyAgent
    {
        public FinancialInstitutionId FinInstnId { get; set; }
    }

    public class PartyAccount
    {
        public PartyAccountId Id { get; set; }
    }

    public class PartyAccountId
    {
        public string IBAN { get; set; }
    }

    public class Party
    {
        public string Nm { get; set; }
        public PostalAddress PstlAdr { get; set; }
        public PartyId Id { get; set; }
    }

    public class PartyId
    {
        public OrganizationId OrgId { get; set; }
    }

    public class OrganizationId
    {
        public OtherOrgId Othr { get; set; }
    }

    public class OtherOrgId
    {
        public string Id { get; set; }
    }

    public class PostalAddress
    {
        public string Ctry { get; set; }
    }

    public class PaymentTypeInformation
    {
        public ServiceLevel SvcLvl { get; set; }
    }

    public class ServiceLevel
    {
        public string Cd { get; set; }
    }

    public class PaymentId
    {
        public string InstrId { get; set; }
        public string EndToEndId { get; set; }
        public string TxId { get; set; }
    }

    public class Agent
    {
        public FinancialInstitutionId FinInstnId { get; set; }
    }

    public class FinancialInstitutionId
    {
        public string BIC { get; set; }
    }

    public class SettlementInformation
    {
        public string SttlmMtd { get; set; }
        public ClrSys ClrSys { get; set; }
    }

    public class ClrSys
    {
        public string Prtry { get; set; }
    }
}
