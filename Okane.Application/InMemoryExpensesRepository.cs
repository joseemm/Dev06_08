using Okane.Domain;

namespace Okane.Application;

public class InMemoryExpensesRepository : IExpensesRepository
{
    private int _nextId = 1;
    private readonly IList<Expense> _expenses;
    private readonly Func<DateTime> _getCurrentTime;

    public InMemoryExpensesRepository(Func<DateTime> getCurrentTime) : this(new List<Expense>(), getCurrentTime)
    {
    }

    private InMemoryExpensesRepository(IList<Expense> expenses, Func<DateTime> getCurrentTime)
    {
        _expenses = expenses;
        _getCurrentTime = getCurrentTime;
    }


    public void Add(Expense expense)
    {
        expense.Id = _nextId++;
        _expenses.Add(expense);
    }

    public IEnumerable<Expense> Search(string? categoryName = null) => 
        categoryName != null 
            ? _expenses.Where(expense => expense.CategoryName == categoryName) 
            : _expenses;

    public void Delete(int id)
    {
        var expenseToDelete = _expenses
            .First(expense => expense.Id == id);
        _expenses.Remove(expenseToDelete);
    }

    public Expense? ById(int id) => 
        _expenses.FirstOrDefault(expense => expense.Id == id);

    public Expense Update(int id, UpdateExpenseRequest request, Category category)
    {
        // TODO: Add test for not found
        var expense = _expenses.First(e => e.Id == id);

        expense.Category = category;
        expense.Amount = request.Amount;
        expense.Description = request.Description;
        expense.UpdatedAt = _getCurrentTime();

        return expense;
    }

    public int Count() => _expenses.Count;
}