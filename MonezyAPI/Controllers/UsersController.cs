using Humanizer.DateTimeHumanizeStrategy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonezyAPI.Data;
using MonezyAPI.Models;

namespace MonezyAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly UsersContext usersContext;

        public UsersController(UsersContext usersContext) 
        {
            this.usersContext = usersContext;
        }

        [HttpGet]
        public ActionResult<List<Users>> GetUsers()
        {
            return usersContext.Users.ToList();
        }

        [HttpGet("IdUser")]
        public async Task<ActionResult<Users>> GetUserById(int UserId)
        { 
            var user = await usersContext.Users.FindAsync(UserId);

            if (user == null)
            {
                return BadRequest();
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<Users>> InsertUsers(Users user)
        {
            usersContext.Users.Add(user);
            await usersContext.SaveChangesAsync();

            return CreatedAtAction(nameof(InsertUsers), new { id = user.IdUser }, user);
        }

    }
}
