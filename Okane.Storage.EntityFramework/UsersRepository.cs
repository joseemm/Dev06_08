using Okane.Application;
using Okane.Domain;

namespace Okane.Storage.EntityFramework;

public class UsersRepository : IUsersRepository
{
    private readonly OkaneDbContext _db;

    public UsersRepository(OkaneDbContext db) => _db = db;

    public void Add(User user)
    {
        _db.Users.Add(user);
        _db.SaveChanges();
    }

    public User? ByEmail(string email) => 
        _db.Users.FirstOrDefault(user => user.Email == email);

    public User ById(int id) => 
        _db.Users.First(user => user.Id == id);
}