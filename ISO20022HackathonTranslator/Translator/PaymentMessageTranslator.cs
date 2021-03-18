using ISO20022HackathonTranslator.Mapping;
using ISO20022HackathonTranslator.Models;
using ISO20022HackathonTranslator.Models.Mx00800102;
using System.IO;
using System.Xml.Serialization;

namespace ISO20022HackathonTranslator.Translator
{
    public static class PaymentMessageTranslator
    {
        public static Document TranslateToMxMessage(MtMessage mtMessage)
        {
            var paymentMessage = MtMessageMapper.ToPaymentMessage(mtMessage);
            var mxMessage = MxMessageMapper.ToMxMessage(paymentMessage);
            return mxMessage;
        }

        public static void WriteMxFile(Document mxMessage, string mxXmlFilePath)
        {
            var xs = new XmlSerializer(typeof(Document));
            using var tw = new StreamWriter(mxXmlFilePath);
            xs.Serialize(tw, mxMessage);
        }
    }
}
