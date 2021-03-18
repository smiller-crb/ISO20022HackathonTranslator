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
                        MsgId = message.Id
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
