using Microsoft.EntityFrameworkCore;
using SampleBankTransactions.Model;
using System.Security.Principal;

namespace SampleBankTransactions.DAL
{
    public class AccountRepository : IAccountRepository, IDisposable
    {
        private BankTransactions context;
        private bool disposed = false;

        public AccountRepository(BankTransactions context)
        {
            this.context = context;
        }
        public void Delete(string accountNumber)
        {
            var account = context.Accounts.FirstOrDefault(x => x.AccountNumber == accountNumber);
            if(account == null)
            {
                throw new Exception($"Account Number {accountNumber} not found for delete");
            }
            var transactions = context.Transactions.Where(x => x.AccountNumber == accountNumber).ToList();
            if(transactions.Any())
                throw new Exception($"Account Number {accountNumber} can not delete, because it has transactions");
            
            context.Accounts.Remove(account);
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

        public AccountForDisplay Get(string accountNumber)
        {
            var account = context.Accounts.FirstOrDefault(x => x.AccountNumber == accountNumber);
            AccountForDisplay toReturn = null;
            if(account != null)
            {
                var customer = context.Customers.FirstOrDefault(x => x.Id == account.CustomerId);

                toReturn = new AccountForDisplay();
                toReturn.AccountNumber = account.AccountNumber;
                toReturn.Type = account.AccountType;
                toReturn.StartAmount = account.StartAmount;
                toReturn.Status = account.Status;
                if(customer != null)
                {
                    toReturn.CustomerIdentityDocument = customer.IdentityDocument;
                    toReturn.Name = customer.Name;
                }
            }
            return toReturn;
        }

        public IEnumerable<AccountForDisplay> GetAll()
        {
            var query = from account in context.Accounts
                        join customer in context.Customers
                        on account.CustomerId equals customer.Id
                        into joined
                        from customer in joined.DefaultIfEmpty()
                        select new AccountForDisplay 
                        { 
                            AccountNumber = account.AccountNumber, 
                            Type = account.AccountType, 
                            StartAmount = account.StartAmount, 
                            Name = customer == null? String.Empty : customer.Name , 
                            CustomerIdentityDocument = customer == null ? String.Empty : customer.IdentityDocument, 
                            Status = account.Status
                        };

            return query.ToList();
        }

        public void Insert(AccountForDisplay account)
        {
            var accountFound = context.Accounts.FirstOrDefault(x => x.AccountNumber == account.AccountNumber);
            if (accountFound == null)
            {
                var customer = context.Customers
                    .FirstOrDefault(x => x.IdentityDocument.Equals( account.CustomerIdentityDocument ));
                if(customer == null)
                {
                    throw new Exception($"Customer with identity document {account.CustomerIdentityDocument} not found");
                }
                context.Accounts.Add(new Account 
                { 
                    AccountNumber = account.AccountNumber, 
                    AccountType = account.Type, 
                    StartAmount = account.StartAmount, 
                    CustomerId = customer.Id,
                    Status = account.Status
                });
                if(account.StartAmount > 0)
                context.Transactions.Add(new Transaction 
                {
                    AccountNumber = account.AccountNumber, 
                    Amount = account.StartAmount, 
                    Balance = account.StartAmount, 
                    CreateDate = DateTimeOffset.Now
                });
            }
            else 
            throw new Exception($"Account Number {account.AccountNumber} already exists");
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(AccountForDisplay account)
        {
            var accountFound = context.Accounts.FirstOrDefault(x => x.AccountNumber == account.AccountNumber);
            if (accountFound == null)
            {
                throw new KeyNotFoundException($"Account Number {account.AccountNumber} not found for update");
            }
            else
            {
                if (accountFound.StartAmount != account.StartAmount)
                    throw new Exception($"Account Number {account.AccountNumber} can not update StartAmount");

                bool modified = false;
                if (accountFound.AccountType != account.Type)
                {
                    modified = true;
                    accountFound.AccountType = account.Type;
                }
                
                if (accountFound.Status != account.Status)
                {
                    modified = true;
                    accountFound.Status = account.Status;
                }
                if (modified)
                    context.Entry(accountFound).State = EntityState.Modified;
            }
        }
    }
}
