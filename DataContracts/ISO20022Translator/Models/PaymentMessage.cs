using DataContracts.ISO20022Translator.Enums;
using System;

namespace ISO20022HackathonTranslator.Models
{
    public class PaymentMessage
    {
        public string MessageId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string SettlementDate { get; set; }
        public string ClearingSystemProprietaryPurpose { get; set; }
        public string SettlementMethod { get; set; }

        // Transaction level
        public string TransactionId { get; set; }
        public string Initiator { get; set; }                             // - REQUIRED Initiator ID. This is the Initiator sent in the CosPartnerConfiguration  
        public Rail Rail { get; set; }                                    // - REQUIRED Payment rail (enum: Internal / XPay / ACH / Wire)
        public string InitiatorName { get; set; }                         // - OPTIONAL (Use this field if you want to override the one in Partner configuration)
        public string InitiatorIdentification { get; set; }               // - OPTIONAL (Use this field if you want to override the one in Partner configuration)
        public string InitiatorOrganizationId { get; set; }
        public string InitiatorRouting { get; set; }                      // - REQUIRED
        public string InitiatorAccount { get; set; }                      // - REQUIRED
        public string InitiatorCountry { get; set; }
        public string SubjectName { get; set; }                           // - OPTIONAL
        public string SubjectIdentification { get; set; }                 // - OPTIONAL
        public string SubjectOrganizationId { get; set; }
        public string SubjectRouting { get; set; }                        // - REQUIRED
        public string SubjectAccount { get; set; }                        // - REQUIRED
        public string SubjectCountry { get; set; }
        public string Description { get; set; }                           // - REQUIRED (length will be cut to supported length in payment processor: ACH - 10 chars, Wire - 50 char, xPay - ?, Internal - ?. Ask COS Support?
        public string AdditionalDescription { get; set; }                 // - OPTIONAL (relevent just for wires)
        public decimal Amount { get; set; }                               // - REQUIRED (in dollars)
        public string ServiceLevel { get; set; }
        public string ChargeBearer { get; set; }
    }
}
