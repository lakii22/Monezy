namespace MonezyAPI.Models
{
    public class Expenses
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public float Value { get; set; }
        public int Type { get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Date { get; set;}
        public int UserId { get; set; }

    }
}
