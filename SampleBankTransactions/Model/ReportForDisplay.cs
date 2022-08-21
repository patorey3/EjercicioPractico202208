namespace SampleBankTransactions.Model
{
    public class ReportForDisplay : TransactionForDisplay
    {
        public string CustomerName { get; set; }
        public bool CustomerStatus { get; set; }
        public bool AccountStatus { get; set; }
        public decimal StartAmount { get; set; }
    }
}
