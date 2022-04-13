using DotnetWsRef.Domain;

namespace DotnetWsRef.Infra.Data;

public interface IUserRepository
{
    Task<IEnumerable<UserModel>> GetUsers();

    Task<UserModel> AddUser(UserModel user);
}

internal class UserRepository : IUserRepository
{
    private readonly AppDbContext _dbContext;

    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserModel> AddUser(UserModel user)
    {
        var trackedUser = await _dbContext.Users.AddAsync(user);

        try
        {
            await _dbContext.SaveChangesAsync();
        }
        catch
        {
            trackedUser.State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            throw;
        }

        return trackedUser.Entity;
    }

    public Task<IEnumerable<UserModel>> GetUsers()
    {
        return Task.FromResult(_dbContext.Users.AsEnumerable());
    }
}