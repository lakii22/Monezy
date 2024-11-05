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


        //get all
        [HttpGet]
        public ActionResult<List<Users>> GetUsers()
        {
            return usersContext.Users.ToList();
        }


        //get by id
        [HttpGet("{IdUser}")]
        public async Task<ActionResult<Users>> GetUserById(int IdUser)
        {
            var user = await usersContext.Users.FindAsync(IdUser);

            if (user == null)
            {
                return BadRequest();
            }

            return Ok(user);
        }


        //Create User
        [HttpPost]
        public async Task<ActionResult<Users>> InsertUsers(Users user)
        {
            usersContext.Users.Add(user);
            await usersContext.SaveChangesAsync();

            return CreatedAtAction(nameof(InsertUsers), new { id = user.IdUser }, user);
        }

        //update user
        [HttpPatch("{IdUser}")]
        public async Task<IActionResult> PatchUser(int IdUser, Users user)
        {
            if (IdUser != user.IdUser)
            {
                return BadRequest();
            }

            usersContext.Entry(user).State = EntityState.Modified;

            try
            {
                await usersContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (user == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //Delete
        [HttpDelete("{IdUser}")]
        public async Task<IActionResult> DeleteUser(int IdUser)
        {
            var user = await usersContext.Users.FindAsync(IdUser);

            if (user == null) 
            {
                return NotFound(); 
            }

            usersContext.Users.Remove(user);
            await usersContext.SaveChangesAsync();
            
            return NoContent();
        }

    }
}
