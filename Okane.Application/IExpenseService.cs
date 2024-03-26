namespace Okane.Application;

public interface IExpenseService
{
    ExpenseResponse Register(CreateExpenseRequest request);
    ExpenseResponse? ById(int id);
    IEnumerable<ExpenseResponse> Search(string? category = null);
    bool Delete(int id);
    ExpenseResponse Update(int id, UpdateExpenseRequest request);
}