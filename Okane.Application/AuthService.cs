using Okane.Domain;

namespace Okane.Application;

public class AuthService : IAuthService
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUsersRepository _usersRepository;
    private readonly ITokenGenerator _tokenGenerator;

    public AuthService(
        IPasswordHasher passwordHasher, 
        IUsersRepository usersRepository, 
        ITokenGenerator tokenGenerator)
    {
        _passwordHasher = passwordHasher;
        _usersRepository = usersRepository;
        _tokenGenerator = tokenGenerator;
    }

    public UserResponse SignUp(SignUpRequest request)
    {
        var hashedPassword = _passwordHasher.Hash(request.Password);
        
        var user = new User
        {
            Email = request.Email,
            HashedPassword = hashedPassword
        };

        _usersRepository.Add(user);

        return new UserResponse
        {
            Id = user.Id,
            Email = user.Email
        };
    }

    public string GenerateToken(SignInRequest request)
    {
        var user = _usersRepository.ByEmail(request.Email)!;

        if (!_passwordHasher.Verify(request.Password, user.HashedPassword))
            throw new NotImplementedException("Scenario for login failed not implemented");

        return _tokenGenerator.Generate(user);
    }
}