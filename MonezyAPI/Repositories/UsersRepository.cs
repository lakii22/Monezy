using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonezyAPI.Data;
using MonezyAPI.Interfaces;
using MonezyAPI.Models;

namespace MonezyAPI.Repositories
{
    public class UsersRepository : IUserRepository
    {
        private readonly UsersContext usersContext;

        public UsersRepository(UsersContext usercContext)
        {
            this.usersContext = usercContext;
        }

        public ActionResult<List<Users>> GetUsers()
        {
            return usersContext.Users.ToList();
        }

        public async Task<ActionResult<Users>> GetUserById(int IdUser)
        {
            return await usersContext.Users.FirstOrDefaultAsync(e => e.IdUser == IdUser);
        }

        public async Task<ActionResult<Users>> InsertUsers(Users user)
        {
            var result = await usersContext.Users.AddAsync(user);
            await usersContext.SaveChangesAsync();
            
            return result.Entity;

        }

        public async Task<Users?> PatchUser(Users user)
        {
            var userToEdit = await usersContext.Users.FirstOrDefaultAsync(e => e.IdUser == user.IdUser);

            if (userToEdit != null)
            {
                userToEdit.NameUser = user.NameUser;
                userToEdit.SurnameUser = user.SurnameUser;
                
                await usersContext.SaveChangesAsync();

                return userToEdit;
            }

            return null;
            
        }

        public async Task DeleteUser(int IdUser)
        {
            var userToDelete = await usersContext.Users.FirstOrDefaultAsync(e => e.IdUser == IdUser);

            if (userToDelete != null)
            {
                usersContext.Users.Remove(userToDelete);
                await usersContext.SaveChangesAsync();
            }

        }

    }
}
