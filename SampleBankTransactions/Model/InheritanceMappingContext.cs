using Microsoft.EntityFrameworkCore;

namespace SampleBankTransactions.Model
{
    public class InheritanceMappingContext
    {
        public DbSet<Person> Persons { get; set; }
    }
}
