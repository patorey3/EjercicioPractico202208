namespace SampleBankTransactions.Model
{
    public enum TransactionTypeEnum
    {
        deposito,
        retiro
    }
    public class TransactionForDisplay
    {
        public DateTimeOffset CreateDate { get; set; }
        public string AccountNumber { get; set; }
        public TransactionTypeEnum TypeOfMove { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
    }
}
