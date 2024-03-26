using Okane.Domain;

namespace Okane.Application;

public interface IExpensesRepository
{
    void Add(Expense expense);
    IEnumerable<Expense> Search(string? categoryName = null);
    void Delete(int id);
    Expense? ById(int id);
    Expense Update(int id, UpdateExpenseRequest request, Category category);
}