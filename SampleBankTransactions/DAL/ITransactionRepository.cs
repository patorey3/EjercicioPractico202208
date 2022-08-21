using SampleBankTransactions.Model;

namespace SampleBankTransactions.DAL
{
    public interface ITransactionRepository : IDisposable
    {
        IEnumerable<Model.ReportForDisplay> Get(DateTimeOffset since, DateTimeOffset until);
        IEnumerable<Model.ReportForDisplay> Get(string IdentityDocument);
        IEnumerable<Model.ReportForDisplay> Get(string IdentityDocument, DateTimeOffset since, DateTimeOffset until);
        Model.TransactionForDisplay Insert(TransactionForDisplay transaction);
        void Save();
    }
}
