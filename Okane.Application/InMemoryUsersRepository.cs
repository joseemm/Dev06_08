using Okane.Domain;

namespace Okane.Application;

public class InMemoryUsersRepository : IUsersRepository
{
    private int _nextId = 1;
    private readonly IList<User> _users;

    public InMemoryUsersRepository() : this(new List<User>())
    {
    }

    private InMemoryUsersRepository(IList<User> users) => 
        _users = users;

    public void Add(User user)
    {
        user.Id = _nextId++;
        _users.Add(user);
    }

    public User? ByEmail(string email) => 
        _users.FirstOrDefault(user => user.Email == email);

    public User ById(int id) => _users.First();
}