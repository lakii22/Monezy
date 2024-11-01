using System.ComponentModel.DataAnnotations;

namespace MonezyAPI.Models
{
    public class Expenses
    {
        [Key]
        public int? IdExpense { get; set; }
        public string? NameExpense { get; set; }
        public string? DescriptionExpense { get; set; }
        public float ValueExpense { get; set; }
        public int TypeExpense { get; set; }
        public DateTime? CreatedExpense { get; set; }
        public DateTime? DateExpense { get; set;}
        public int UserId { get; set; }

    }
}
