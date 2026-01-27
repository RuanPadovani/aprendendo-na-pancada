using IdentityService.Application.Interfaces;
using IdentityService.Domain.Entities;
using IdentityService.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<User>> GetAll()
    {
        return await _context.Users.ToListAsync();

    }

    public async Task<User> GetUserById(Guid id)
    {
#pragma warning disable CS8603 // Possible null reference return.
        return await _context.Users.Where(c => c.UserId == id).FirstOrDefaultAsync();
#pragma warning restore CS8603 // Possible null reference return.
    }

    public async Task<User> CreateUser(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> EditUser(User user)
    {
        _context.Entry(user).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> DeleteUser(Guid id)
    {
        var removeUser = await GetUserById(id);
        _context.Users.Remove(removeUser);
        await _context.SaveChangesAsync();
        return removeUser;
    }


}
