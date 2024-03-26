using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Okane.Application;
using Okane.Domain;

namespace Okane.Storage.EntityFramework;

public class ExpensesRepository : IExpensesRepository
{
    private readonly OkaneDbContext _db;
    private readonly Func<DateTime> _getCurrentTime;

    public ExpensesRepository(OkaneDbContext db, Func<DateTime> getCurrentTime)
    {
        _db = db;
        _getCurrentTime = getCurrentTime;
    }

    public void Add(Expense expense)
    {
        _db.Expenses.Add(expense);
        _db.SaveChanges();
    }

    public IEnumerable<Expense> Search(string? categoryName = null)
    {
        var expensesWithCategories = _db.Expenses
            .Include(expense => expense.Category)
            .ToArray();
        
        return categoryName != null
            ? expensesWithCategories
                .Where(expense => expense.CategoryName == categoryName)
            : expensesWithCategories;
    }

    public void Delete(int id)
    {
        var expenseToDelete = _db.Expenses
            .First(expense => expense.Id == id);

        _db.Expenses.Remove(expenseToDelete);
        _db.SaveChanges();
    }

    public Expense? ById(int id) => 
        _db.Expenses
            .Include(expense => expense.Category)
            .FirstOrDefault(expense => expense.Id == id);
    
    public Expense Update(int id,  UpdateExpenseRequest request, Category category)
    {
        var expense = _db.Expenses.First(e => e.Id == id);

        expense.Category = category;
        expense.Amount = request.Amount;
        expense.Description = request.Description;
        expense.UpdatedAt = _getCurrentTime();

        _db.SaveChanges();
        return expense;
    }
}