using ISO20022HackathonTranslator.MTParser;
using System.IO;
using Xunit;

namespace ISO20022HackathonTranslatorTests
{
    public class MTParserTests
    {
        [Fact]
        public void Parse_Valid_ShouldReturnValidMtMessage()
        {
            // Arrange
            var mt103Data1 = 
@"{1:F01SOGEFRPPAXXX0070970817}{2:O1031734150713DEUTDEFFBXXX00739698421607131634N}{3:{103:TGT}{108:OPTUSERREF16CHAR}}{4:
:20:UNIQUEREFOFTRX16
:23B:CRED
:32A:180724EUR735927,75
:33B:EUR735927,75
:50A:/DE37500700100950596700
DEUTDEFF
:59:/FR7630003034950005005419318
CHARLES DUPONT COMPANY
RUE GENERAL DE GAULLE, 21
75013 PARIS
:71A:SHA
-}{5:{CHK:D628FE0165A7}}";

            var mt103Data2 =
@"{1:F01SOGEFRPPAXXX0070970817}{2:O1031734150713DEUTDEFFBXXX00739698421607131634N}{3:{103:TGT}{108:OPTUSERREF16CHAR}}{4: // not sure about this line yet
:20:F912912084329372
:23B:CRED
:32A:201229USD20,
:33B:USD20,
:50F:/IL370540910000910115697
1/ORDRING NAME
2/STREET 8 20
3/IL/CITY 17000                         
:57D://FW121000248
WELLS FARGO BANK, N.A.
:59F:/1037786507                
1/BEN NAME                              
2/STREET                               
3/US/BELLEVUE WA 98008                  
:71A:OUR 
-}{5:{CHK:D628FE0165A7}} // not sure about this line yet";

            var stream = MtReader.GenerateStreamFromString(mt103Data2);
            var reader = new MtReader(stream);

            // Act
            var message = reader.Parse();

            // Assert
            Assert.Equal("", message.ApplicationHeader.MessageType);
        }
    }
}
