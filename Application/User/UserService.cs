using DotnetWsRef.Domain;
using DotnetWsRef.Infra.Data;
using DotnetWsRef.Infra.Web;

namespace DotnetWsRef.Application.User;

public interface IUserService
{
    Task<UserModel> AddUser(UserModel user);

    Task<IEnumerable<UserModel>> GetUsers();
}

internal class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailAgent _emailAgent;

    public UserService(IUserRepository userRepository, IEmailAgent emailAgent)
    {
        _userRepository = userRepository;
        _emailAgent = emailAgent;
    }

    public async Task<UserModel> AddUser(UserModel user)
    {
        _ = _emailAgent.SendEmail($"Please verify your account, {user.Name}");

        return await _userRepository.AddUser(user);
    }

    public Task<IEnumerable<UserModel>> GetUsers()
    {
        return _userRepository.GetUsers();
    }
}