using ISO20022HackathonTranslator.Models;

using System;
using System.IO;
using System.Linq;
using System.Text;

namespace ISO20022HackathonTranslator.MTParser
{
    public class MtReader : IMtReader
    {
        private readonly StreamReader reader;

        public MtReader(Stream stream)
        {
            reader = new StreamReader(stream);
        }

        public MtReader(string path)
        {
            reader = new StreamReader(path);
        }

        public MtMessage Parse()
        {
            MtMessage message = new MtMessage();
            int block = 0;
            int level = 0;
            bool isLabel = false;

            while (true)
            {
                var intValue = reader.Read();
                if (intValue < 0)
                    break;
                char character = (char) intValue;

                if (character == '{')
                {
                    level++;
                    if (level == 1)
                        isLabel = true;
                }

                if (Char.IsDigit(character) && isLabel)
                {
                    block = (int) Char.GetNumericValue(character);
                }

                if (character == ':' && isLabel)
                {
                    isLabel = false;
                    switch (block)
                    {
                        case 1:
                            message.Header = GetHeader();
                            level--;
                            break;
                        case 2:
                            message.ApplicationHeader = GetApplicationHeader();
                            level--;
                            break;
                        case 3:
                            message.UserHeader = GetUserHeader();
                            level--;
                            break;
                        case 4:
                            message.Body = GetBody();
                            level--;
                            break;
                        case 5:
                            message.Trailer = GetTrailer();
                            level--;
                            break;
                    }
                    block = 0;
                }
            }

            return message;
        }

        private MtMessageHeader GetHeader()
        {
            MtMessageHeader header = new MtMessageHeader();
            var content = ReadUntil(':', '}').Replace(" ", "");
            header.AppId = content.Substring(0, 1);
            header.ServiceId = content.Substring(1, 2);
            header.LogicalTerminalAddress = content.Substring(3, 12);
            header.SessionNumber = content.Substring(15, 4);
            header.SequenceNumber = content.Substring(19, 6);
            return header;
        }

        private MtMessageApplicationHeader GetApplicationHeader()
        {
            MtMessageApplicationHeader applicationHeader = new MtMessageApplicationHeader();
            return applicationHeader;
        }

        private MtMessageUserHeader GetUserHeader()
        {
            MtMessageUserHeader userHeader = new MtMessageUserHeader();

            while (true)
            {
                var content = ReadUntil('}');

                if (string.IsNullOrEmpty(content))
                    break;

                var column = content.IndexOf(':');
                var label = content.Substring(0, column);
                var value = content.Substring(column + 2);

                switch (label)
                {
                    case "103":
                        userHeader.ServiceIdentifier = value;
                        break;
                    case "113":
                        userHeader.BankingPriority = value;
                        break;
                    case "108":
                        userHeader.MessageUserReference = value;
                        break;
                    case "119":
                        userHeader.ValidationFlag = value;
                        break;
                    case "111":
                        userHeader.ServiceTypeIdentifier = value;
                        break;
                    case "121":
                        userHeader.EndToEndTransactionReference = value;
                        break;
                    case "433":
                        userHeader.SanctionsScreeningInformation = value;
                        break;
                    case "434":
                        userHeader.PaymentControlsInformation = value;
                        break;
                }
            }

            return userHeader;
        }

        private MtMessageBody GetBody()
        {
            MtMessageBody messageBody = new MtMessageBody();
            var label = "";
            var value = "";

            do
            {
                label = ReadUntil(':', '}')?.ToUpper();
                value = ReadUntil(':', '}');

                if (value == null)
                    continue;

                switch (label)
                {
                    case "20":
                        messageBody.SenderReference = value;
                        break;
                    case "13C":
                        break;
                    case "23B":
                        messageBody.BankOperationCode = value
                        switch
                        {
                            "CRED" => BankOperationCode.NormalCreditTransfer,
                            "CRTS" => BankOperationCode.TestMessage,
                            "SPAY" => BankOperationCode.SWIFTPay,
                            "SPRI" => BankOperationCode.Priority,
                            "SSTD" => BankOperationCode.Standard,
                            _ => null
                        };
                        break;
                    case "23E":
                        break;
                    case "26T":
                        messageBody.TransactionTypeCode = value;
                        break;
                    case "32A":
                        messageBody.ValueDate = value.Substring(0, 6);
                        messageBody.SettledCurrency = value.Substring(6, 3);
                        messageBody.InterbankSettledAmount = value.Substring(9);
                        break;
                    case "33B":
                        messageBody.InstructedCurrency = value.Substring(0, 3);
                        messageBody.InstructedAmount = value.Substring(3);
                        break;
                    case "36":
                        messageBody.ExchangeRate = value;
                        break;
                    case "50F":
                        messageBody.OrderingCustomer = ParseCustomer(value);
                        break;
                    case "57D":
                        messageBody.AccountWithInstitution = value;
                        break;
                    case "59F":
                        messageBody.BeneficiaryCustomer = ParseCustomer(value);
                        break;
                    case "71A":
                        messageBody.DetailsOfCharges = value;
                        break;
                }
            } while (!(label == null && value == null));

            return messageBody;
        }

        private MtMessageTrailer GetTrailer()
        {
            MtMessageTrailer trailer = new MtMessageTrailer();
            return trailer;
        }

        private string ReadUntil(params char[] delimiters)
        {
            StringBuilder stringBuilder = new StringBuilder();

            while (true)
            {
                var intValue = reader.Read();
                if (intValue < 0)
                    return null;
                char character = (char) intValue;
                if (delimiters.Contains(character))
                    break;
            }

            return stringBuilder.ToString();
        }

        private MtCustomer ParseCustomer(string value)
        {
            MtCustomer customer = new MtCustomer();

            string[] lines = value.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            customer.PartyIndentifier = lines[0];
            if (customer.PartyIndentifier.StartsWith("/"))
                customer.PartyIndentifier = customer.PartyIndentifier.Substring(1);

            for (var line = 1; line < lines.Length; line++)
            {
                switch (lines[line].Substring(0, 1))
                {
                    case "1":
                        customer.Name = lines[line].Substring(2);
                        break;
                    case "2":
                        customer.Address = lines[line].Substring(2);
                        break;
                    case "3":
                        var countryTown = lines[line].Substring(2);
                        var firstSlash = countryTown.IndexOf('/');
                        customer.Country = countryTown.Substring(0, firstSlash);
                        customer.Town = countryTown.Substring(firstSlash + 2);
                        break;
                }
            }

            return customer;
        }

        public void Dispose()
        {
            reader.Dispose();
        }
    }
}
