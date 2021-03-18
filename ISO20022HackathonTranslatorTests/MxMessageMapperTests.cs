using ISO20022HackathonTranslator.Mapping;
using ISO20022HackathonTranslator.Models;
using Xunit;

namespace ISO20022HackathonTranslatorTests
{
    public class MxMessageMapperTests
    {
        [Fact]
        public void ToMxMessage_Valid_ShouldMapAllRelevantFields()
        {
            // Arrange
            var paymentMessage = new PaymentMessage
            {
                Id = "Message ID 123"
            };

            // Act
            var mxMessage = MxMessageMapper.ToMxMessage(paymentMessage);

            // Assert
            Assert.Equal(paymentMessage.Id, mxMessage.FIToFICstmrCdtTrf.GrpHdr.MsgId);
        }
    }
}
