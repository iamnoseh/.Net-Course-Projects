using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.CtegoriesDto;

public class CreateCategoriesDto
{
    [Required]
    public required string  Name{get;set;}
}