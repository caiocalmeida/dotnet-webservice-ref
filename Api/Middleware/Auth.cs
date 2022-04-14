using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace DotnetWsRef.Api.Middleware;

[AttributeUsage(AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    private readonly AuthType _type;

    public AuthorizeAttribute(AuthType type)
    {
        _type = type;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (_type == AuthType.ApiKey)
        {
            if (context.HttpContext.Request.Headers.TryGetValue("X-API-KEY", out var apiKey) && apiKey == "example")
            {
                return;
            }

            context.Result = new ContentResult
            {
                StatusCode = (int)HttpStatusCode.Unauthorized,
                Content = "Please provide a valid key in the X-API-KEY request header. (Since this is just an example API, use \"example\" as the key)"
            };

            return;
        }

        context.Result = new ContentResult
        {
            StatusCode = (int)HttpStatusCode.Forbidden,
            Content = "This API endpoint is unavailable in order to prevent abuse."
        };
    }
}

public static class GraphQlAuth
{
    public static bool Authorize(AuthType authType, HttpContext context, out string? errorMessage)
    {
        errorMessage = null;

        if (authType == AuthType.ApiKey)
        {
            if (context.Request.Headers.TryGetValue("X-API-KEY", out var apiKey) && apiKey == "example")
            {
                return false;
            }

            errorMessage = "Unauthorized query. Please provide a valid key in the X-API-KEY request header. (Since this is just an example API, use \"example\" as the key)";
            return true;
        }

        errorMessage = "This query is unavailable in order to prevent abuse.";
        return true;
    }
}

public enum AuthType
{
    Forbid = 0,
    ApiKey = 1,
}