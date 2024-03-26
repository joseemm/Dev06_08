namespace Okane.Application;

public class UpdateExpenseRequest
{
    public required string CategoryName { get; set; }
    public int Amount { get; set; }
    public string? Description { get; set; }
}