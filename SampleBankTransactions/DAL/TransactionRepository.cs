using SampleBankTransactions.Model;

namespace SampleBankTransactions.DAL
{
    public class TransactionRepository : ITransactionRepository, IDisposable
    {
        private BankTransactions context;
        private bool disposed = false;

        public TransactionRepository(BankTransactions context)
        {
            this.context = context;
        }

        public void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerable<ReportForDisplay> Get(DateTimeOffset since, DateTimeOffset until)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReportForDisplay> Get(string IdentityDocument)
        {
            var customerFound = context.Customers.FirstOrDefault(x => x.IdentityDocument.Equals(IdentityDocument));
            if(customerFound == null)
            {
                throw new Exception($"Customer with identity document {IdentityDocument} not found");
            }
            var accountsFound = context.Accounts.Where(x => x.CustomerId == customerFound.Id);
            if(accountsFound.Count() == 0)
            {
                throw new Exception($"Account number for {customerFound.Name} not found");
            }

            var query = from transaction in context.Transactions
                        join account in accountsFound
                        on transaction.AccountNumber equals account.AccountNumber
                        orderby transaction.CreateDate
                        select new ReportForDisplay
                        {
                            CreateDate = transaction.CreateDate,
                            AccountNumber = account.AccountNumber,
                            AccountStatus = account.Status,
                            Amount = transaction.Amount,
                            Balance = transaction.Balance,
                            StartAmount = transaction.Balance - transaction.Amount,
                            CustomerName = customerFound.Name,
                            CustomerStatus = customerFound.Status,
                            TypeOfMove = transaction.Amount > 0 ? TransactionTypeEnum.deposito : TransactionTypeEnum.retiro
                        };
            return query.ToList();
        }

        public IEnumerable<ReportForDisplay> Get(string IdentityDocument, DateTimeOffset since, DateTimeOffset until)
        {
            var customerFound = context.Customers.FirstOrDefault(x => x.IdentityDocument.Equals(IdentityDocument));
            if (customerFound == null)
            {
                throw new Exception($"Customer with identity document {IdentityDocument} not found");
            }
            var accountsFound = context.Accounts.Where(x => x.CustomerId == customerFound.Id);
            if (accountsFound.Count() == 0)
            {
                throw new Exception($"Account number for {customerFound.Name} not found");
            }

            var query = from transaction in context.Transactions
                        .Where(x => x.CreateDate >= since.Date && x.CreateDate <= until.Date)
                        join account in accountsFound
                        on transaction.AccountNumber equals account.AccountNumber
                        orderby transaction.CreateDate
                        select new ReportForDisplay
                        {
                            CreateDate = transaction.CreateDate,
                            AccountNumber = account.AccountNumber,
                            AccountStatus = account.Status,
                            Amount = transaction.Amount,
                            Balance = transaction.Balance,
                            StartAmount = transaction.Balance - transaction.Amount,
                            CustomerName = customerFound.Name,
                            CustomerStatus = customerFound.Status,
                            TypeOfMove = transaction.Amount > 0 ? TransactionTypeEnum.deposito : TransactionTypeEnum.retiro
                        };
            return query.ToList();
        }

        public TransactionForDisplay Insert(TransactionForDisplay transaction)
        {
            var accountFound = context.Accounts.FirstOrDefault(x =>x.AccountNumber == transaction.AccountNumber); 

            if(accountFound == null)
            {
                throw new Exception($"Account {transaction.AccountNumber} not found");
            }
            var customerFound = context.Customers.FirstOrDefault(x => x.Id == accountFound.CustomerId);
            if (customerFound == null)
            {
                throw new Exception($"Customer with Account {transaction.AccountNumber} not found");
            }

            var transactionsForRespectiveAccount
                = context.Transactions.Where(x => x.AccountNumber.Equals(transaction.AccountNumber));

            decimal balance = 0;
            balance = transactionsForRespectiveAccount.Sum(x => x.Amount);
            if (transaction.TypeOfMove == TransactionTypeEnum.retiro)
            {
                if (transaction.Amount > balance)
                    throw new Exception($"Balance for account {transaction.AccountNumber} = {balance}, not enough funds");
            }

            decimal amount = transaction.TypeOfMove == TransactionTypeEnum.deposito ?
                transaction.Amount : transaction.Amount * (-1);
            var newTransaction = new Transaction 
            {
                CreateDate = DateTimeOffset.Now,
                AccountNumber = transaction.AccountNumber, 
                Amount = amount, 
                Balance = balance + amount
            };

            context.Transactions.Add(newTransaction);

            transaction.Balance = newTransaction.Balance;
            return transaction;
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
