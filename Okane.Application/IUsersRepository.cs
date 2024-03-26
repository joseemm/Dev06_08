using Okane.Domain;

namespace Okane.Application;

public interface IUsersRepository
{
    void Add(User user);
    User? ByEmail(string email);
    User ById(int id);
}