using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Okane.Application;

namespace Okane.WebApi.Controllers;

[AllowAnonymous]
[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService) => 
        _authService = authService;

    // POST /signup
    [HttpPost("signup")]
    public UserResponse Post(SignUpRequest request) => 
        _authService.SignUp(request);
    
    // Get /token
    [HttpPost("token")]
    public ActionResult Get(SignInRequest request) => 
        Ok(new { Token = _authService.GenerateToken(request) });
}