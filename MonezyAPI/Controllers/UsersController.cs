using Humanizer.DateTimeHumanizeStrategy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonezyAPI.Data;
using MonezyAPI.Interfaces;
using MonezyAPI.Models;
using MonezyAPI.Repositories;

namespace MonezyAPI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly IUserRepository userRepository;

        public UsersController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        //get all
        [HttpGet]
        public ActionResult<List<Users>> GetUsers()
        {
            return userRepository.GetUsers();
        }

        //get by id
        [HttpGet("{IdUser}")]
        public async Task<ActionResult<Users>> GetUserById(int IdUser)
        {
            var user = await userRepository.GetUserById(IdUser);

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
            try
            {
                if (user == null)
                {
                    return BadRequest();
                }

                var createdUser = await userRepository.InsertUsers(user);

                return CreatedAtAction(nameof(GetUsers), new { id = user.IdUser }, createdUser.Value);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error inserting new user");
            }
        }

        //update user
        [HttpPatch("{IdUser}")]
        public async Task<ActionResult<Users>> PatchUser(int IdUser, Users user)
        {
            try
            {
                if (IdUser != user.IdUser)
                {
                    return BadRequest("User id mismatch");
                }

                var userToUpdate = await userRepository.GetUserById(IdUser);

                if (userToUpdate == null)
                {
                    return NotFound($"User with the id {IdUser} not found");
                }

                return await userRepository.PatchUser(user);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating user data");
            }
        }

        //Delete
        [HttpDelete("{IdUser}")]
        public async Task<IActionResult> DeleteUser(int IdUser)
        {
            try
            {
                var userToDelete = await userRepository.GetUserById(IdUser);
                if (userToDelete == null)
                {
                    return NotFound($"User with id {IdUser} not found");
                }

                await userRepository.DeleteUser(IdUser);

                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting user from the server");
            }
        }

    }
}
