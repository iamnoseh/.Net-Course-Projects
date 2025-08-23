namespace Domain.Filter;

public class CourierFilter : BaseFilter
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public bool? IsAvailable { get; set; }
    
}