using Okane.Application;
using Okane.Domain;
using Okane.Storage.EntityFramework;

public class CategoriesRepository : ICategoriesRepository
{
    private readonly OkaneDbContext _db;

    public CategoriesRepository(OkaneDbContext db) => _db = db;

    public Category ByName(string categoryName) => 
        _db.Categories.First(category => category.Name == categoryName);
}