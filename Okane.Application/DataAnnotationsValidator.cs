using System.ComponentModel.DataAnnotations;

namespace Okane.Application;

public class DataAnnotationsValidator<T>
{
    public IDictionary<string, string[]> Validate(T obj)
    {
        var results = new List<ValidationResult>();

        var context = new ValidationContext(
            obj!, serviceProvider: null, items: null);

        Validator.TryValidateObject(obj!, context, results, validateAllProperties: true);

        return results
            .GroupBy(result => string.Join('+', result.MemberNames))
            .ToDictionary(
                grouping => grouping.Key, 
                ToArray);
    }
    
    private static string[] ToArray(IGrouping<string, ValidationResult> grouping) => 
        grouping.Select(result => result.ErrorMessage ?? string.Empty).ToArray();
}