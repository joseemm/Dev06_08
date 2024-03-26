using Okane.Application;
using Okane.Domain;

namespace Okane.Tests;

public class CreateExpenseRequestValidationsTests
{
    private readonly DataAnnotationsValidator<CreateExpenseRequest> _validator;

    public CreateExpenseRequestValidationsTests() => 
        _validator = new DataAnnotationsValidator<CreateExpenseRequest>();

    [Fact]
    public void Valid()
    {
        var validationResults = _validator
                .Validate(new CreateExpenseRequest
                {
                    Amount = 10,
                    CategoryName = "DefaultCategory",
                    InvoiceUrl = "http://invoice.com/123"
                });
        
        Assert.Empty(validationResults);
    }
    
    [Fact]
    public void InvalidAmount()
    {
        var validationResults = _validator
            .Validate(new CreateExpenseRequest
            {
                Amount = 0,
                CategoryName = "DefaultCategory"
            });
        
        var (property, errors) = Assert.Single(validationResults);
        
        Assert.Equal("Amount", property);
        Assert.Equal("Amount is out of range", errors.First());
    }
    
    [Fact]
    public void CategoryTooLong()
    {
        var validationResults = _validator
            .Validate(new CreateExpenseRequest
            {
                Amount = 10,
                CategoryName = string.Join("", Enumerable.Repeat("a", 100))
            });
        
        var (property, errors) = Assert.Single(validationResults);
        
        Assert.Equal(nameof(CreateExpenseRequest.CategoryName), property);
        Assert.Equal("Category is too long", errors.First());
    }
    
    [Fact]
    public void DescriptionTooLong()
    {
        var validationResults = _validator
            .Validate(new CreateExpenseRequest
            {
                Amount = 10,
                CategoryName = "DefaultCategory",
                Description = string.Join("", Enumerable.Repeat("a", 500))
            });
        
        var (property, errors) = Assert.Single(validationResults);
        
        Assert.Equal("Description", property);
        Assert.Equal("Description is too long", errors.First());
    }
    
    [Fact]
    public void InvalidUrl()
    {
        var validationResults = _validator
            .Validate(new CreateExpenseRequest
            {
                Amount = 10,
                CategoryName = "DefaultCategory",
                InvoiceUrl = "Invalid"
            });
        
        var (property, errors) = Assert.Single(validationResults);
        
        Assert.Equal(nameof(Expense.InvoiceUrl), property);
        Assert.Contains("The InvoiceUrl field is not a valid", errors.First());
    }
}