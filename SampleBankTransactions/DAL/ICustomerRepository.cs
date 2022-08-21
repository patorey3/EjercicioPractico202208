namespace SampleBankTransactions.DAL
{
    public interface ICustomerRepository : IDisposable
    {
        IEnumerable<Model.CustomerForDisplay> GetAll();
        Model.CustomerForDisplay Get(string IdentityDocument);
        void Insert(Model.CustomerForDisplay customer);
        void Update(Model.CustomerForDisplay customer);
        void Delete(string IdentityDocument);
        void Save();
    }
}
