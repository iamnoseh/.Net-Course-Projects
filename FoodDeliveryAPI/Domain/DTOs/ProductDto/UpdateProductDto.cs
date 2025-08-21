﻿namespace Domain.DTOs.ProductDto;

public class UpdateProductDto
{
    public int Id{get;set;}
    public string  Name{get;set;}    
    public string  Description{get;set;}
    public decimal  Price{get;set;}
    public int    CategoryId{get;set;}
    public bool IsAvailable{get;set;}
}