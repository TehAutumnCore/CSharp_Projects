using System.ComponentModel.DataAnnotations;//provides attribute classes that are used to define meta data for ASPNET data controls
namespace ExpenseTracker.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public decimal Value { get; set; }

        [Required] //description is required
        public string? Description { get; set; } //? allows for null - nullable
    }
}