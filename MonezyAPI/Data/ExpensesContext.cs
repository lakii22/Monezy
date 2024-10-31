using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MonezyAPI.Models;

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
