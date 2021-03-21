using ISO20022HackathonTranslator.Models;
using ISO20022HackathonTranslator.Models.Mx00800102;
using System;

namespace ISO20022HackathonTranslator.Mapping
{
    public static class MxMessageMapper
    {
        public static Document ToMxMessage(PaymentMessage message)
        {
            var mxMessage = new Document
            {
                FIToFICstmrCdtTrf =new Transaction
                {
                    GrpHdr = new GroupHeader
                    {
                        MsgId = message.MessageId,
                        CreDtTm = message.CreatedDate?.ToString(),
                        NbOfTxs = 1,
                        IntrBkSttlmDt = message.SettlementDate?.ToString(),
                        TtlIntrBkSttlmAmt = message.Amount,
                        SttlmInf = new SettlementInformation
                        {
                            ClrSys = new ClrSys
                            {
                                Prtry = message.ClearingSystemProprietaryPurpose
                            },
                            SttlmMtd = message.SettlementMethod
                        },
                        InstgAgt = new Agent
                        {
                            FinInstnId = new FinancialInstitutionId
                            {
                                BIC = message.InitiatorIdentification // "INGSMMXXX"
                            }
                        },
                        InstdAgt = new Agent
                        {
                            FinInstnId = new FinancialInstitutionId
                            {
                                BIC = message.SubjectIdentification //  "BTRLRO22"
                            }
                        }
                    },
                    CdtTrfTxInf = new CreditTransferTransactionInformation[]
                    {
                        new CreditTransferTransactionInformation
                        {
                            PmtId = new PaymentId
                            {
                                InstrId = message.TransactionId, // "PaymentID"
                                EndToEndId = message.TransactionId, // "PaymentID"
                                TxId = message.TransactionId // "PaymentID"
                            },
                            PmtTpInf = new PaymentTypeInformation
                            {
                                SvcLvl = new ServiceLevel
                                {
                                    Cd = message.ServiceLevel // "SEPA"
                                }
                            },
                            IntrBkSttlmAmt = message.Amount, // "1"
                            ChrgBr = message.ChargeBearer, // "SLEV"
                            Dbtr = new Party
                            {
                                Nm = message.InitiatorName, // "Jane Doe"
                                PstlAdr = new PostalAddress
                                {
                                    Ctry = message.InitiatorCountry // "ES"
                                },
                                Id = new PartyId
                                {
                                    OrgId = new OrganizationId
                                    {
                                        Othr = new OtherOrgId
                                        {
                                            Id = message.InitiatorOrganizationId // "GFA"
                                        }
                                    }
                                }
                            },
                            DbtrAcct = new PartyAccount
                            {
                                Id = new PartyAccountId
                                {
                                    IBAN = message.InitiatorAccount // "RO10INGBCBN4EVKBNFJ8YNGT"
                                }
                            },
                            DbtrAgt = new PartyAgent
                            {
                                FinInstnId = new FinancialInstitutionId
                                {
                                    BIC = message.InitiatorRouting // "CECAESMMXXX"
                                }
                            },
                            CdtrAgt = new PartyAgent
                            {
                                FinInstnId = new FinancialInstitutionId
                                {
                                    BIC = message.SubjectRouting // "BTRLRO22"
                                }
                            },
                            Cdtr = new Party
                            {
                                Nm = message.SubjectName, // "John Doe"
                                PstlAdr = new PostalAddress
                                {
                                    Ctry = message.SubjectCountry // "ES"
                                },
                                Id = new PartyId
                                {
                                    OrgId = new OrganizationId
                                    {
                                        Othr = new OtherOrgId
                                        {
                                            Id = message.SubjectOrganizationId // "V94025590"
                                        }
                                    }
                                }
                            },
                            CdtrAcct = new PartyAccount
                            {
                                Id = new PartyAccountId
                                {
                                    IBAN = message.SubjectAccount // "RO71BTRLV67M9G4XI3IEJ5D2"
                                }
                            },
                            RmtInf = new RemittanceInformation
                            {
                                Ustrd = message.Description // "Incoming Payment"
                            }
                        }
                    }
                }
            };

            return mxMessage;
        }

        public static PaymentMessage ToPaymentMessage(Document mxMessage)
        {
            throw new NotImplementedException();
        }
    }
}
