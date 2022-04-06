using DotnetWsRef.Application.User;

namespace DotnetWsRef.Api.GraphQL
{
    public partial class Query
    {
        public async Task<IEnumerable<UserDto>> Users([Service] IUserService userService)
        {
            return (await userService.GetUsers()).Select(u => UserDto.From(u));
        }
    }

    public partial class Mutation
    {
        public async Task<UserDto> AddUser([Service] IUserService userService, UserDto input)
        {
            return UserDto.From(await userService.AddUser(input.NewUser()));
        }
    }
}