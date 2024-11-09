using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MonezyAPI.Data;
using MonezyAPI.Interfaces;
using MonezyAPI.Models;

namespace MonezyAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ExpensesController : Controller
    {
        private readonly IExpensesRepository expensesRepository;

        public ExpensesController(IExpensesRepository expensesRepository)
        {
            //_context = context;
            this.expensesRepository = expensesRepository;
        }


        //get all
        [HttpGet]
        public ActionResult<List<Expenses>> GetExpenses()
        {
            return expensesRepository.GetExpenses();
        }


        //get one by Id
        [HttpGet("{IdExpenses}")]
        public async Task<ActionResult<Expenses>> GetExpensesById(int IdExpenses)
        {
            var expense = await expensesRepository.GetExpensesById(IdExpenses); 

            if (expense == null)
            {
                return BadRequest();
            }

            return Ok(expense.Value);
        }

        //get by user
        [HttpGet("{search}/{UserId}")]
        public async Task<ActionResult<IEnumerable<Expenses>>> GetExpensesByUser(int UserId)
        {
            try
            {
                var expenses = await expensesRepository.GetExpensesByUser(UserId);

                if (expenses.Any())
                {
                    return Ok(expenses);
                }

                return NotFound();
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the server");
            }


        }

        //add expense
        [HttpPost]
        public async Task<ActionResult<Expenses>> InsertExpense(Expenses expense)
        {
            try
            {
                if (expense == null)
                {
                    return BadRequest();
                }
                
                var createdExpense = await expensesRepository.InsertExpense(expense);


                return CreatedAtAction(nameof(GetExpenses), new { id = expense.IdExpense}, createdExpense.Value);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error inserting new expense record.");
            }
        }

        //Edit Expense
        [HttpPatch("{IdExpense}")]
        public async Task<ActionResult<Expenses>> EditExpense(int IdExpense, Expenses expense)
        {
            try
            {
                if (IdExpense != expense.IdExpense)
                {
                    return BadRequest("Expense ID Mismatch");
                }

                var expenseToUpdate = await expensesRepository.GetExpensesById(IdExpense);

                if (expenseToUpdate == null)
                {
                    return NotFound($"Expense with Id {IdExpense} not found");
                }

                return await expensesRepository.PatchExpense(expense);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error editing Expense");
            }
        }

        //delete Expense
        [HttpDelete("{IdExpense:int}")]
        public async Task<IActionResult> DeleteExpense(int IdExpense)
        {
            try
            {
                var expenseToDelete = expensesRepository.GetExpensesById(IdExpense);

                if (expenseToDelete == null)
                {
                    return NotFound($"Expense with Id {IdExpense} not found");
                }

                await expensesRepository.DeleteExpense(IdExpense);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting expense");            
            }
        }
    }
        
}
