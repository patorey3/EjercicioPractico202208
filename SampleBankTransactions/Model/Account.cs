using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleBankTransactions.Model
{
    public class Account
    {
        [Key]
        [MaxLength(25)]
        public string AccountNumber { get; set; }
        [MaxLength(250)]
        public string AccountType { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal StartAmount { get; set; }
        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
        public bool Status { get; set; }
    }
}
