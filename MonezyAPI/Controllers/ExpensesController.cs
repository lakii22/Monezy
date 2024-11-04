using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MonezyAPI.Data;
using MonezyAPI.Models;

namespace MonezyAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ExpensesController : Controller
    {
        private readonly ExpensesContext _context;

        public ExpensesController(ExpensesContext context)
        {
            _context = context;
        }

        [HttpGet]
        public void Get(int id)
        {
            
        }

        [HttpPost]
        public async Task<ActionResult<Expenses>> InsertExpense(Expenses expense) {
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(InsertExpense), new { id = expense.IdExpense }, expense);
        }
    }
}
