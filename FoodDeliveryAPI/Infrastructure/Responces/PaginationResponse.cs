﻿using System.Net;
using Infrastructure.Responce;

namespace Infrastructure.Responces;

public class PaginationResponse<T> : Responce<T>
{
    public int PageNumber {get;set;}
    public int PageSize {get;set;}
    public int TotalPages {get;set;}
    public int TotalRecords {get;set;}
    
    public PaginationResponse(T data, int totalRecords,int pageNumber,int pageSize) : base(data)
    {
        TotalPages= (int)Math.Ceiling((double)totalRecords / pageSize);
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalRecords = totalRecords;
    }
    public PaginationResponse(HttpStatusCode statusCode, string message) : base(statusCode, message)
    {
    }
}