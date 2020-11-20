using System;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Domain.Data
{
    public class Expense
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime TransactionTime { get; set; }
        [Required]
        [Range(0, 1000000)]
        public decimal Amount { get; set; }
        [Required]
        public string Recipient { get; set; }
        [Required]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Currency must be a 3 character string")]
        public string Currency { get; set; }
        public ExpenseType ExpenseType { get; set; }

    }

    public enum ExpenseType
    {
        Other = 0,
        Food,
        Drinks
    }
}
