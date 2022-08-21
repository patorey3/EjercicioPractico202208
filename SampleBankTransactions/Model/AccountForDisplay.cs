namespace SampleBankTransactions.Model
{
    public class AccountForDisplay
    {
        public string AccountNumber { get; set; }
        public string Type { get; set; }
        public decimal StartAmount { get; set; }
        public string CustomerIdentityDocument { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
    }
}
