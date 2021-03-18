using DataContracts.ISO20022Translator.Enums;

namespace ISO20022HackathonTranslator.Models
{
    public class PaymentMessage
    {
        public string Id { get; set; }
        public string Initiator { get; set; }                             // - REQUIRED Initiator ID. This is the Initiator sent in the CosPartnerConfiguration  
        //removed correlationId and batch fields
        public Rail Rail { get; set; }                                    // - REQUIRED Payment rail (enum: Internal / XPay / ACH / Wire)
        //removed Direction, serviceLevel, PaymentClassification fields     
        public string InitiatorName { get; set; }                         // - OPTIONAL (Use this field if you want to override the one in Partner configuration)
        public string InitiatorIdentification { get; set; }               // - OPTIONAL (Use this field if you want to override the one in Partner configuration)
        public string InitiatorRouting { get; set; }                      // - REQUIRED
        public string InitiatorAccount { get; set; }                      // - REQUIRED
        public string SubjectName { get; set; }                           // - OPTIONAL
        public string SubjectIdentification { get; set; }                 // - OPTIONAL
        public string SubjectRouting { get; set; }                        // - REQUIRED
        public string SubjectAccount { get; set; }                        // - REQUIRED
        //removed SubjectAccountType since only for ach
        public string Description { get; set; }                           // - REQUIRED (length will be cut to supported length in payment processor: ACH - 10 chars, Wire - 50 char, xPay - ?, Internal - ?. Ask COS Support?
        public string AdditionalDescription { get; set; }                 // - OPTIONAL (relevent just for wires)
        public decimal Amount { get; set; }                               // - REQUIRED (in dollars)
    }
}
