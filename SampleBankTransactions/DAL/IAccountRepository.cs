using SampleBankTransactions.Model;

namespace SampleBankTransactions.DAL
{
    public interface IAccountRepository : IDisposable
    {
        IEnumerable<Model.AccountForDisplay> GetAll();
        Model.AccountForDisplay Get(string accountNumber);
        void Insert(AccountForDisplay account);
        void Update(AccountForDisplay account);
        void Delete(string accountNumber);
        void Save();
    }
}
