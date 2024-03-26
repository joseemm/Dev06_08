using Moq;
using Okane.Application;
using Okane.Domain;

namespace Okane.Tests;

public class AuthServiceTests
{
    private readonly IAuthService _authService;
    private readonly InMemoryUsersRepository _usersRepository;

    public AuthServiceTests()
    {
        var mockPasswordHasher = new Mock<IPasswordHasher>(MockBehavior.Strict);

        mockPasswordHasher
            .Setup(hasher => hasher.Hash("1234"))
            .Returns("TestHash");

        mockPasswordHasher.Setup(hasher => 
                hasher.Verify("1234", "TestHash"))
            .Returns(true);

        var mockTokenGenerator = new Mock<ITokenGenerator>(MockBehavior.Strict);
        mockTokenGenerator
            .Setup(generator => generator.Generate(It.IsAny<User>()))
            .Returns("FakeToken");

        _usersRepository = new InMemoryUsersRepository();
        _authService = new AuthService(mockPasswordHasher.Object, _usersRepository,
            mockTokenGenerator.Object);
    }

    [Fact]
    public void SignUp()
    {
        var createdUser = _authService.SignUp(new SignUpRequest
        {
            Email = "user@mail.com",
            Password = "1234"
        });

        Assert.Equal(1, createdUser.Id);
        Assert.Equal("user@mail.com", createdUser.Email);

        var user = _usersRepository.ById(1);
        Assert.Equal("TestHash", user.HashedPassword);
    }

    [Fact]
    public void GenerateToken()
    {
        var createdUser = _authService.SignUp(new SignUpRequest
        {
            Email = "user@mail.com",
            Password = "1234"
        });

        var token = _authService.GenerateToken(new SignInRequest
        {
            Email = "user@mail.com",
            Password = "1234"
        });

        Assert.Equal("FakeToken", token);
    }
}