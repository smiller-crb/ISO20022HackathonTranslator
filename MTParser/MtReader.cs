using ISO20022HackathonTranslator.Models;

using System;
using System.Collections.Generic;
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
            ReadUntil('}');
            return applicationHeader;
        }

        private MtMessageUserHeader GetUserHeader()
        {
            MtMessageUserHeader userHeader = new MtMessageUserHeader();

            while (true)
            {
                var content = ReadUntil('}');
                if (content.StartsWith("{"))
                    content = content.Substring(1);

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

            var content = ReadUntil('}');
            string[] linesArray = content.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            Array.Reverse(linesArray);
            var lines = new Stack<string>(linesArray);

            var label = "";
            var option = "";

            while (lines.Count > 0)
            {
                var line = lines.Pop();

                if (line.StartsWith("-"))
                    continue;

                var valueStart = 0;
                if (line.StartsWith(":"))
                {
                    label = line.Substring(1, 2);
                    if (line[3] == ':')
                    {
                        option = "";
                        valueStart = 4;
                    }
                    else
                    {
                        option = line.Substring(3, 1);
                        valueStart = 5;
                    }
                }
                var value = line.Substring(valueStart);

                if (value == null)
                    continue;

                switch (label)
                {
                    case "20":
                        messageBody.SenderReference = value;
                        break;
                    case "13":
                        break;
                    case "23":
                        if (option == "B")
                        {
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
                        }
                        break;
                    case "26":
                        messageBody.TransactionTypeCode = value;
                        break;
                    case "32":
                        messageBody.ValueDate = value.Substring(0, 6);
                        messageBody.SettledCurrency = value.Substring(6, 3);
                        messageBody.InterbankSettledAmount = value.Substring(9);
                        break;
                    case "33":
                        messageBody.InstructedCurrency = value.Substring(0, 3);
                        messageBody.InstructedAmount = value.Substring(3);
                        break;
                    case "36":
                        messageBody.ExchangeRate = value;
                        break;
                    case "50":
                        messageBody.OrderingCustomer = ParseCustomer(value, option, lines);
                        break;
                    case "57":
                        messageBody.AccountWithInstitution = value;
                        break;
                    case "59":
                        messageBody.BeneficiaryCustomer = ParseCustomer(value, option, lines);
                        break;
                    case "71":
                        if (option == "A")
                            messageBody.DetailsOfCharges = value;
                        break;
                }
            }

            return messageBody;
        }

        private MtMessageTrailer GetTrailer()
        {
            MtMessageTrailer trailer = new MtMessageTrailer();
            ReadUntil('}');
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
                stringBuilder.Append(character);
            }

            return stringBuilder.ToString();
        }

        private MtCustomer ParseCustomer(string value, string option, Stack<string> lines)
        {
            MtCustomer customer = new MtCustomer();

            customer.PartyIndentifier = value;
            if (customer.PartyIndentifier.StartsWith("/"))
                customer.PartyIndentifier = customer.PartyIndentifier.Substring(1);

            while (lines.Count > 0)
            {

                var line = lines.Pop();

                if (!(line.StartsWith(":") || line.StartsWith("-")))
                {
                    if (option == "F")
                    {
                        switch (line.Substring(0, 1))
                        {
                            case "1":
                                customer.Name = line.Substring(2);
                                break;
                            case "2":
                                customer.Address = line.Substring(2);
                                break;
                            case "3":
                                var countryTown = line.Substring(2);
                                var firstSlash = countryTown.IndexOf('/');
                                customer.Country = countryTown.Substring(0, firstSlash);
                                customer.Town = countryTown.Substring(firstSlash + 2);
                                break;
                        }
                    }
                }
                else
                {
                    lines.Push(line);
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
