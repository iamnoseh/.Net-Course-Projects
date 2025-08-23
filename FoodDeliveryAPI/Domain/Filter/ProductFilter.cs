namespace Domain.Filter;

public class ProductFilter : BaseFilter
{
    public string? Name { get; set; }
    public decimal?  MinPrice {get;set;}
    public decimal?  MaxPrice {get;set;}
    public bool? IsAvailable {get;set;}
    public string? CategoryName {get;set;}
}