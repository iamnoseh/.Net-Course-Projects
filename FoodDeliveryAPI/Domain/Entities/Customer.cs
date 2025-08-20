using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Customer
{
    public int Id {get;set;}
    [Required]
    [StringLength(50,MinimumLength =2)]
    public string FullName {get;set;}
    [Required]
    public string Email {get;set;}
}