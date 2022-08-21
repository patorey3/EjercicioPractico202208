using System.ComponentModel.DataAnnotations;

namespace SampleBankTransactions.Model
{
    public class CustomerForDisplay
    {
        public string IdentityDocument { get; set; }
        public string Name { get; set; }
        public string? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }
        public bool Status { get; set; }
        //public string? AccountNumber { get; set; }
    }
}
