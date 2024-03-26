using Okane.Domain;

namespace Okane.Application;

public class InMemoryCategoriesRepository : ICategoriesRepository
{
    private readonly IList<Category> _categories;

    public InMemoryCategoriesRepository() : this(new List<Category>())
    {
    }

    private InMemoryCategoriesRepository(IList<Category> categories) => 
        _categories = categories;

    public Category ByName(string categoryName) => 
        _categories.First(category => category.Name == categoryName);

    public void Add(Category category) => 
        _categories.Add(category);
}