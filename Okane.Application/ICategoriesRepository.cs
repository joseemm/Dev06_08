using Okane.Domain;

namespace Okane.Application;

public interface ICategoriesRepository
{
    Category ByName(string categoryName);
}