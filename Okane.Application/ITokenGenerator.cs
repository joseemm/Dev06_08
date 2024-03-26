using Okane.Domain;

namespace Okane.Application;

public interface ITokenGenerator
{
    string Generate(User user);
}