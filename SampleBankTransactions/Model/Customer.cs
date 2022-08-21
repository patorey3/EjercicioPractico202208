using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleBankTransactions.Model
{
    public class Customer : Person
    {
        [MaxLength(250)]
        public string? Password { get; set; }
        public bool Status { get; set; }
    }
}
