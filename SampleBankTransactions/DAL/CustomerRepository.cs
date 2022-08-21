using Microsoft.EntityFrameworkCore;
using SampleBankTransactions.Model;
using System.Security.Principal;

namespace SampleBankTransactions.DAL
{
    public class CustomerRepository : ICustomerRepository, IDisposable
    {
        private BankTransactions context;
        private bool disposed = false;

        public CustomerRepository(BankTransactions context)
        {
            this.context = context;
        }

        public void Delete(string IdentityDocument)
        {
            var customerFound = context.Customers
                .FirstOrDefault(x => x.IdentityDocument.Equals(IdentityDocument));
            if(customerFound == null)
            {
                throw new Exception($"Customer with identity document {IdentityDocument} not found for delete");
            }

            var accountFound = context.Accounts.FirstOrDefault(x => x.CustomerId == customerFound.Id);
            if(accountFound != null)
            {
                var transactions = context.Transactions.Where(x => x.AccountNumber == accountFound.AccountNumber).ToList();
                if (transactions.Any())
                {
                    throw new Exception($"Customer {customerFound.Name} can not delete, because has transactions");
                }                    
            }

            context.Customers.Remove(customerFound);
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

        public CustomerForDisplay Get(string IdentityDocument)
        {
            CustomerForDisplay toReturn = null;

            var customer = context.Customers
                .FirstOrDefault(x => x.IdentityDocument.Equals(IdentityDocument));

            if(customer != null)
            {
                toReturn = new CustomerForDisplay();
                toReturn.IdentityDocument = customer.IdentityDocument;
                toReturn.Name = customer.Name;
                toReturn.Address = customer.Address;
                toReturn.BirthDate = customer.BirthDate;
                toReturn.Gender = customer.Gender;
                toReturn.Phone = customer.Phone;
                toReturn.Status = customer.Status;
            }

            return toReturn;
        }

        public IEnumerable<CustomerForDisplay> GetAll()
        {
            return context.Customers
                .Select(x => new CustomerForDisplay 
                { 
                    IdentityDocument = x.IdentityDocument, 
                    Name = x.Name, 
                    Gender = x.Gender, 
                    Phone = x.Phone,
                    Password = "*******",
                    Status = x.Status, 
                    Address = x.Address, 
                    BirthDate = x.BirthDate,
                })
                .ToList();
        }

        public void Insert(CustomerForDisplay customer)
        {
            var person = context.Persons
                .FirstOrDefault(x => x.IdentityDocument.Equals(customer.IdentityDocument));
            var newCustomer = new Customer
            {
                IdentityDocument = customer.IdentityDocument,
                Name = customer.Name,
                Address = customer.Address,
                BirthDate = customer.BirthDate,
                Gender = customer.Gender,
                Password = customer.Password,
                Phone = customer.Phone,
                Status = customer.Status,
            };


            if (person == null)
            {
                context.Customers.Add(newCustomer);
                return;
            }
            var customerFound = context.Customers
                .FirstOrDefault(x => x.Id == person.Id);
            if(customerFound == null)
            {
                newCustomer.Id = person.Id;
                context.Customers.Add(newCustomer);
            }
            else
            {
                throw new Exception($"Customer already exists Document:{customerFound.IdentityDocument} Name:{customerFound.Name}");
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(CustomerForDisplay customer)
        {
            var customerFound = context.Customers
                .FirstOrDefault(x => x.IdentityDocument.Equals( customer.IdentityDocument));

            if (customerFound == null)
            {
                throw new KeyNotFoundException($"Account Number {customer.IdentityDocument} not found for update");
            }
            else
            {
                bool modified = false;
                if (!customerFound.Name.Equals(customer.Name))
                {
                    customerFound.Name = customer.Name;
                    modified = true;
                }
                if(customerFound.Status != customer.Status)
                {
                    customerFound.Status = customer.Status;
                    modified = true;
                }
                if((!string.IsNullOrEmpty(customerFound.Password) && string.IsNullOrEmpty( customer.Password))
                    || !customerFound.Password.Equals(customer.Password))
                {
                    customerFound.Password = customer.Password;
                    modified = true;
                }
                if (!customerFound.Address.Equals(customer.Address))
                {
                    customerFound.Address = customer.Address;
                    modified = true;
                }
                if (!customerFound.Phone.Equals(customer.Phone))
                {
                    customerFound.Phone = customer.Phone;
                    modified = true;
                }
                if (!customerFound.Gender.Equals(customer.Gender))
                {
                    customerFound.Gender = customer.Gender;
                    modified = true;
                }
                if (!customerFound.Status!=customer.Status)
                {
                    customerFound.Status = customer.Status;
                    modified = true;
                }
                if (modified)
                {
                    context.Entry(customerFound).State = EntityState.Modified;
                }
            }
        }
    }
}
