using System.Security.Claims;
using Okane.Application;

namespace Okane.WebApi;

public class HttpContextUserSession : IUserSession
{
    private readonly IHttpContextAccessor _contextAccessor;

    public HttpContextUserSession(IHttpContextAccessor contextAccessor) => 
        _contextAccessor = contextAccessor;

    public int GetCurrentUserId()
    {
        var nameIdentifierClaim = _contextAccessor
            .HttpContext!
            .User
            .FindFirst(ClaimTypes.NameIdentifier);

        return int.Parse(nameIdentifierClaim!.Value);
    }
}