using Microsoft.EntityFrameworkCore;

namespace MonezyAPI.Data
{
    public class ExpensesContext : DbContext
    {
        public ExpensesContext (DbContextOptions<ExpensesContext> options)
            : base(options)
        {
        }

        public DbSet<MonezyAPI.Models.Expenses> Expenses { get; set; } = default!;
    }
}
