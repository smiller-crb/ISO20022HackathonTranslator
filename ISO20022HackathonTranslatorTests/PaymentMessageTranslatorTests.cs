using ISO20022HackathonTranslator.Models.Mx00800102;
using ISO20022HackathonTranslator.Translator;

using System.IO;
using System.Xml.Serialization;

using Xunit;

namespace ISO20022HackathonTranslatorTests
{
    public class PaymentMessageTranslatorTests
    {
        [Fact]
        public void WriteMxFile_Valid_ShouldWriteToMxXmlFile()
        {
            // Arrange
            var mxXmlFilePath = @"../../../Output/mxMessage.xml";
            var mxMessage = new Document
            {
                FIToFICstmrCdtTrf = new Transaction
                {
                GrpHdr = new GroupHeader
                {
                MsgId = "Message Id 123",
                CreDtTm = "2019-12-23T09:40:13",
                NbOfTxs = 1,
                TtlIntrBkSttlmAmt = 1,
                IntrBkSttlmDt = "2019-12-23",
                SttlmInf = new SettlementInformation
                {
                SttlmMtd = "CLRG",
                ClrSys = new ClrSys
                {
                Prtry = "ST2"
                }
                },
                InstgAgt = new Agent
                {
                FinInstnId = new FinancialInstitutionId
                {
                BIC = "INGSMMXXX"
                }
                },
                InstdAgt = new Agent
                {
                FinInstnId = new FinancialInstitutionId
                {
                BIC = "BTRLRO22"
                }
                }
                },
                CdtTrfTxInf = new CreditTransferTransactionInformation[]
                {
                new CreditTransferTransactionInformation
                {
                PmtId = new PaymentId
                {
                InstrId = "PaymentID",
                EndToEndId = "PaymentID",
                TxId = "PaymentID"
                },
                PmtTpInf = new PaymentTypeInformation
                {
                SvcLvl = new ServiceLevel
                {
                Cd = "SEPA"
                }
                },
                IntrBkSttlmAmt = 1,
                ChrgBr = "SLEV",
                Dbtr = new Party
                {
                Nm = "Jane Doe",
                PstlAdr = new PostalAddress
                {
                Ctry = "ES"
                },
                Id = new PartyId
                {
                OrgId = new OrganizationId
                {
                Othr = new OtherOrgId
                {
                Id = "GFA"
                }
                }
                }
                },
                DbtrAcct = new PartyAccount
                {
                Id = new PartyAccountId
                {
                IBAN = "RO10INGBCBN4EVKBNFJ8YNGT"
                }
                },
                DbtrAgt = new PartyAgent
                {
                FinInstnId = new FinancialInstitutionId
                {
                BIC = "CECAESMMXXX"
                }
                },
                CdtrAgt = new PartyAgent
                {
                FinInstnId = new FinancialInstitutionId
                {
                BIC = "BTRLRO22"
                }
                },
                Cdtr = new Party
                {
                Nm = "John Doe",
                PstlAdr = new PostalAddress
                {
                Ctry = "ES"
                },
                Id = new PartyId
                {
                OrgId = new OrganizationId
                {
                Othr = new OtherOrgId
                {
                Id = "V94025590"
                }
                }
                }
                },
                CdtrAcct = new PartyAccount
                {
                Id = new PartyAccountId
                {
                IBAN = "RO71BTRLV67M9G4XI3IEJ5D2"
                }
                },
                RmtInf = new RemittanceInformation
                {
                Ustrd = "Incoming Payment"
                }
                }
                }
                }
            };

            // Act
            PaymentMessageTranslator.WriteMxFile(mxMessage, mxXmlFilePath);

            // Assert
            using var sr = new StreamReader(mxXmlFilePath);
            var xs = new XmlSerializer(typeof(Document));
            var readMxMessage = (Document) xs.Deserialize(sr);

            Assert.Equal(mxMessage.FIToFICstmrCdtTrf.GrpHdr.MsgId, readMxMessage.FIToFICstmrCdtTrf.GrpHdr.MsgId);
            Assert.Equal(mxMessage.FIToFICstmrCdtTrf.GrpHdr.CreDtTm, readMxMessage.FIToFICstmrCdtTrf.GrpHdr.CreDtTm);
            Assert.Equal(mxMessage.FIToFICstmrCdtTrf.GrpHdr.NbOfTxs, readMxMessage.FIToFICstmrCdtTrf.GrpHdr.NbOfTxs);
            Assert.Equal(mxMessage.FIToFICstmrCdtTrf.GrpHdr.TtlIntrBkSttlmAmt, readMxMessage.FIToFICstmrCdtTrf.GrpHdr.TtlIntrBkSttlmAmt);
            Assert.Equal(mxMessage.FIToFICstmrCdtTrf.GrpHdr.IntrBkSttlmDt, readMxMessage.FIToFICstmrCdtTrf.GrpHdr.IntrBkSttlmDt);
            Assert.Equal(mxMessage.FIToFICstmrCdtTrf.GrpHdr.SttlmInf.SttlmMtd, readMxMessage.FIToFICstmrCdtTrf.GrpHdr.SttlmInf.SttlmMtd);
            Assert.Equal(mxMessage.FIToFICstmrCdtTrf.GrpHdr.SttlmInf.ClrSys.Prtry, readMxMessage.FIToFICstmrCdtTrf.GrpHdr.SttlmInf.ClrSys.Prtry);
            Assert.Equal(mxMessage.FIToFICstmrCdtTrf.GrpHdr.InstgAgt.FinInstnId.BIC, readMxMessage.FIToFICstmrCdtTrf.GrpHdr.InstgAgt.FinInstnId.BIC);
            Assert.Equal(mxMessage.FIToFICstmrCdtTrf.GrpHdr.InstdAgt.FinInstnId.BIC, readMxMessage.FIToFICstmrCdtTrf.GrpHdr.InstdAgt.FinInstnId.BIC);
            Assert.Equal(mxMessage.FIToFICstmrCdtTrf.CdtTrfTxInf[0].PmtId.EndToEndId, readMxMessage.FIToFICstmrCdtTrf.CdtTrfTxInf[0].PmtId.EndToEndId);
            Assert.Equal(mxMessage.FIToFICstmrCdtTrf.CdtTrfTxInf[0].PmtId.InstrId, readMxMessage.FIToFICstmrCdtTrf.CdtTrfTxInf[0].PmtId.InstrId);
            Assert.Equal(mxMessage.FIToFICstmrCdtTrf.CdtTrfTxInf[0].PmtId.TxId, readMxMessage.FIToFICstmrCdtTrf.CdtTrfTxInf[0].PmtId.TxId);
            Assert.Equal(mxMessage.FIToFICstmrCdtTrf.CdtTrfTxInf[0].PmtTpInf.SvcLvl.Cd, readMxMessage.FIToFICstmrCdtTrf.CdtTrfTxInf[0].PmtTpInf.SvcLvl.Cd);
            Assert.Equal(mxMessage.FIToFICstmrCdtTrf.CdtTrfTxInf[0].IntrBkSttlmAmt, readMxMessage.FIToFICstmrCdtTrf.CdtTrfTxInf[0].IntrBkSttlmAmt);
            Assert.Equal(mxMessage.FIToFICstmrCdtTrf.CdtTrfTxInf[0].ChrgBr, readMxMessage.FIToFICstmrCdtTrf.CdtTrfTxInf[0].ChrgBr);
            Assert.Equal(mxMessage.FIToFICstmrCdtTrf.CdtTrfTxInf[0].Dbtr.Nm, readMxMessage.FIToFICstmrCdtTrf.CdtTrfTxInf[0].Dbtr.Nm);
            Assert.Equal(mxMessage.FIToFICstmrCdtTrf.CdtTrfTxInf[0].Dbtr.PstlAdr.Ctry, readMxMessage.FIToFICstmrCdtTrf.CdtTrfTxInf[0].Dbtr.PstlAdr.Ctry);
            Assert.Equal(mxMessage.FIToFICstmrCdtTrf.CdtTrfTxInf[0].Dbtr.Id.OrgId.Othr.Id, readMxMessage.FIToFICstmrCdtTrf.CdtTrfTxInf[0].Dbtr.Id.OrgId.Othr.Id);
            Assert.Equal(mxMessage.FIToFICstmrCdtTrf.CdtTrfTxInf[0].DbtrAcct.Id.IBAN, readMxMessage.FIToFICstmrCdtTrf.CdtTrfTxInf[0].DbtrAcct.Id.IBAN);
            Assert.Equal(mxMessage.FIToFICstmrCdtTrf.CdtTrfTxInf[0].DbtrAgt.FinInstnId.BIC, readMxMessage.FIToFICstmrCdtTrf.CdtTrfTxInf[0].DbtrAgt.FinInstnId.BIC);
            Assert.Equal(mxMessage.FIToFICstmrCdtTrf.CdtTrfTxInf[0].CdtrAgt.FinInstnId.BIC, readMxMessage.FIToFICstmrCdtTrf.CdtTrfTxInf[0].CdtrAgt.FinInstnId.BIC);
            Assert.Equal(mxMessage.FIToFICstmrCdtTrf.CdtTrfTxInf[0].Cdtr.Id.OrgId.Othr.Id, readMxMessage.FIToFICstmrCdtTrf.CdtTrfTxInf[0].Cdtr.Id.OrgId.Othr.Id);
            Assert.Equal(mxMessage.FIToFICstmrCdtTrf.CdtTrfTxInf[0].Cdtr.Nm, readMxMessage.FIToFICstmrCdtTrf.CdtTrfTxInf[0].Cdtr.Nm);
            Assert.Equal(mxMessage.FIToFICstmrCdtTrf.CdtTrfTxInf[0].Cdtr.PstlAdr.Ctry, readMxMessage.FIToFICstmrCdtTrf.CdtTrfTxInf[0].Cdtr.PstlAdr.Ctry);
            Assert.Equal(mxMessage.FIToFICstmrCdtTrf.CdtTrfTxInf[0].CdtrAcct.Id.IBAN, readMxMessage.FIToFICstmrCdtTrf.CdtTrfTxInf[0].CdtrAcct.Id.IBAN);
            Assert.Equal(mxMessage.FIToFICstmrCdtTrf.CdtTrfTxInf[0].RmtInf.Ustrd, readMxMessage.FIToFICstmrCdtTrf.CdtTrfTxInf[0].RmtInf.Ustrd);
        }
    }
}
