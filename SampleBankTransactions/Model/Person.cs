using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SampleBankTransactions.Model
{
    [Index(nameof(IdentityDocument), IsUnique = true)]
    public class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(25)]
        public string IdentityDocument { get; set; }
        [MaxLength(250)]
        [Required]
        public string Name { get; set; }
        [MaxLength(25)]
        public string? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        [MaxLength(250)]
        public string? Address { get; set; }
        [MaxLength(75)]
        public string? Phone { get; set; }

    }
}
