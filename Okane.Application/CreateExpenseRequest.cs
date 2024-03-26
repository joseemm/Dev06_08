using System.ComponentModel.DataAnnotations;

namespace Okane.Application;

public class CreateExpenseRequest
{
    [Required]
    [Range(1, 1_000_000, ErrorMessage = "Amount is out of range")]
    public int Amount { get; set; }
    
    [Required]
    [MaxLength(80, ErrorMessage = "Category is too long")]
    public required string CategoryName { get; set; }
    
    [MaxLength(250, ErrorMessage = "Description is too long")]
    public string? Description { get; set; }
    
    [Url]
    public string? InvoiceUrl { get; set; }
}