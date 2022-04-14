using DotnetWsRef.Api.Middleware;
using DotnetWsRef.Application.User;
using HotChocolate.Resolvers;

namespace DotnetWsRef.Api.GraphQL
{
    public partial class Query
    {
        public async Task<IEnumerable<UserDto>?> Users(
            [Service] IUserService userService,
            [Service] IHttpContextAccessor httpContextAccessor,
            IResolverContext context)
        {
            if (GraphQlAuth.Authorize(AuthType.ApiKey, httpContextAccessor.HttpContext!, out var errorMessage))
            {
                context.ReportError(errorMessage!);
                return null;
            }

            return (await userService.GetUsers()).Select(u => UserDto.From(u));
        }
    }

    public partial class Mutation
    {
        public async Task<UserDto?> AddUser(
            [Service] IUserService userService,
            [Service] IHttpContextAccessor httpContextAccessor,
            IResolverContext context,
            UserDto input)
        {
            if (GraphQlAuth.Authorize(AuthType.Forbid, httpContextAccessor.HttpContext!, out var errorMessage))
            {
                context.ReportError(errorMessage!);
                return null;
            }

            return UserDto.From(await userService.AddUser(input.NewUser()));
        }
    }
}