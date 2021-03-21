using ISO20022HackathonTranslator.MTParser;
using ISO20022HackathonTranslator.Translator;
using System;
using System.IO;
using System.Text;

namespace ISO20022HackathonTranslator
{
    class Program
    {
        private const string mtString = 
@"{1:F01SOGEFRPPAXXX0070970817}{2:O1031734150713DEUTDEFFBXXX00739698421607131634N}{3:{103:TGT}{108:OPTUSERREF16CHAR}}{4:
:20:UNIQUEREFOFTRX16
:23B:CRED
:32A:180724EUR735927,75
:33B:EUR735927,75
:50F:/IL030540510000310044282
1/CUST NAME
2/STREET
3/IL/A CITY
:57A:MIZBILITXXX
:59F:/IL950205480000000160974
1/BEN NAME
2/STREET 4
3/IL/B CITY:71A:SHA
-}{5:{CHK:D628FE0165A7}}";

        static void Main(string[] args)
        {
            var byteArray = Encoding.ASCII.GetBytes(mtString);

            using var stream = new MemoryStream(byteArray);
            using var reader = new MtReader(stream);
            var mtMessage = reader.Parse();

            var mxMessage = PaymentMessageTranslator.TranslateToMxMessage(mtMessage);

            var outputFileLocation = "mxMessage.xml";
            PaymentMessageTranslator.WriteMxFile(mxMessage, outputFileLocation);

            Console.WriteLine($"{mtMessage.Body.InstructedAmount} {mtMessage.Body.InstructedCurrency} sent to {mtMessage.Body.BeneficiaryCustomer.Name}");
        }
    }
}
