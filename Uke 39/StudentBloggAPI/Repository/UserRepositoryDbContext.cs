using Microsoft.EntityFrameworkCore;
using StudentBloggAPI.Data;
using StudentBloggAPI.Models.Entities;
using StudentBloggAPI.Repository.Interfaces;

namespace StudentBloggAPI.Repository;

public class UserRepositoryDbContext : IUserRepository
{
    private readonly StudentBloggDbContext _dbContext;

    public UserRepositoryDbContext(StudentBloggDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<User> AddUserAsync(User user)
    {
        var entityEntry = await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
        return entityEntry.Entity;

    }

    public async Task<User?> DeleteUserByIdAsync(int id)
    {
        using var transcation = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            var userToDelete = await GetUserByIdAsync(id);
            if (userToDelete != null)
            {
                _dbContext.Remove(userToDelete);
                await _dbContext.SaveChangesAsync();
                transcation.Commit();

                return userToDelete;
            }
            return null;
        }
        catch (Exception)
        {
            transcation.Rollback();
            throw;
        }
    }

    public async Task<ICollection<User>> GetPagedUsersAsync(int pageNr, int pageSize)
    {
        var count = await _dbContext.Users.CountAsync();
        if (count == 0)
        {
            return Enumerable.Empty<User>().ToList();
        }

        
        var users = await _dbContext.Users
                .OrderBy(u => u.Id)
                .Skip((pageNr - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        
        return users;
        
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<User?> GetUserByNameAsync(string name)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(x => x.UserName.Equals(name));

        return user;
    }

    public async Task<User?> UpdateUserAsync(int id, User user)
    {

        var userToUpdate = await _dbContext.Users.FindAsync(id);
       
        if (userToUpdate != null && user != null)
        {
            userToUpdate.FirstName = user.FirstName;
            userToUpdate.LastName = user.LastName;
            userToUpdate.Email = user.Email;
            userToUpdate.UserName = user.UserName;
            userToUpdate.Updated = DateTime.Now;

            //_dbContext.Users.Update(userToUpdate);
            await _dbContext.SaveChangesAsync();
          
            return userToUpdate;
        }
        return null;
    }
}
