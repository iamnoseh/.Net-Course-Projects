using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services;

public interface IFileStorageService
{
    Task<string> SaveFileAsync(IFormFile file, string relativeFolder);
    Task DeleteFileAsync(string relativePath);
}