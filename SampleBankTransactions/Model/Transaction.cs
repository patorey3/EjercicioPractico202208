using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleBankTransactions.Model
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        [MaxLength(25)]
        public string AccountNumber { get; set; }
        [ForeignKey("AccountNumber")]
        public virtual Account Account { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Amount { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Balance { get; set; }
    }
}
