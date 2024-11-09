using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonezyAPI.Data;
using MonezyAPI.Interfaces;
using MonezyAPI.Models;

namespace MonezyAPI.Repositories
{
    public class ExpensesRepository : IExpensesRepository
    {
        private readonly ExpensesContext expensesContext;

        public ExpensesRepository(ExpensesContext expensesContext)
        { 
            this.expensesContext = expensesContext;
        }

        public ActionResult<List<Expenses>> GetExpenses()
        {
            return expensesContext.Expenses.ToList();
        }

        public async Task<ActionResult<Expenses>> GetExpensesById(int IdExpenses)
        {
            return await expensesContext.Expenses.FirstOrDefaultAsync(e => e.IdExpense == IdExpenses);
        }

        public async Task<IEnumerable<Expenses>> GetExpensesByUser(int UserId)
        {
            IQueryable<Expenses> query = expensesContext.Expenses;

            if (!string.IsNullOrEmpty(UserId.ToString()))
            {
                query = query.Where(e => e.UserId == UserId);
            }

            return await query.ToListAsync();
        }

        public async Task<ActionResult<Expenses>> InsertExpense(Expenses expense)
        {
            var result = await expensesContext.Expenses.AddAsync(expense);
            await expensesContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Expenses?> PatchExpense(Expenses expense)
        {
            var result = await expensesContext.Expenses.FirstOrDefaultAsync(e => e.IdExpense == expense.IdExpense);

            if (result != null)
            {
                result.NameExpense = expense.NameExpense;
                result.DateExpense = expense.DateExpense;
                result.NameExpense = expense.NameExpense;
                result.TypeExpense = expense.TypeExpense;
                result.DescriptionExpense = expense.DescriptionExpense;
                result.ValueExpense = expense.ValueExpense;

                await expensesContext.SaveChangesAsync();
                
                return result;
            }
            return null;
        }

        public async Task DeleteExpense(int IdExpense)
        {
            var result = await expensesContext.Expenses.FirstOrDefaultAsync(e => e.IdExpense == IdExpense);

            if (result != null)
            {
                expensesContext.Expenses.Remove(result);
                await expensesContext.SaveChangesAsync();
            }
        }
    }
}
