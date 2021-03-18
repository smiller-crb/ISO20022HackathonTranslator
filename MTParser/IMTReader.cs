using ISO20022HackathonTranslator.Models;

using System;

namespace ISO20022HackathonTranslator.MTParser
{
    public interface IMtReader : IDisposable
    {
        MtMessage Parse();
    }
}
