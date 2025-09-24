using Microsoft.AspNetCore.Http;
namespace Infrastructure.Services;

public class FileStorageService(string rootPath) : IFileStorageService
{
    public async Task<string> SaveFileAsync(IFormFile file, string relativeFolder)
    {
        var folder = Path.Combine(rootPath, "wwwroot", relativeFolder);
        if (!Directory.Exists(folder))
            Directory.CreateDirectory(folder);

        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var fullPath = Path.Combine(folder, fileName);// wwwroot/image/126hhsjd-dsfhjs211-sdfbjh12-12.png

        using var stream = new FileStream(fullPath, FileMode.Create);
        await file.CopyToAsync(stream);

        return Path.Combine(relativeFolder, fileName).Replace("\\", "/");
    }

    public Task DeleteFileAsync(string relativePath)
    {
        var full = Path.Combine(rootPath, "wwwroot", relativePath.Replace("/", Path.DirectorySeparatorChar.ToString()));
        if (File.Exists(full)) File.Delete(full);
        return Task.CompletedTask;
    }
}