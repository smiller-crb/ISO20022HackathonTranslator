namespace DataContracts.ISO20022Translator.Enums
{
    public enum Rail
    {
        Unknown,
        Internal,
        XPay,
        ACH,
        ACHCCD,
        ACHPPD,
        Wire,
        DirectDebit
    }

    public static class RailExtensions
    {
        public static bool IsAch(Rail rail)
        {
            return (rail.ToString().StartsWith("ACH") || rail.Equals(Rail.DirectDebit));

        }
    }
}
