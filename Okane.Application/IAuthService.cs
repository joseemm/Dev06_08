namespace Okane.Application;

public interface IAuthService
{
    UserResponse SignUp(SignUpRequest request);
    string GenerateToken(SignInRequest request);
}