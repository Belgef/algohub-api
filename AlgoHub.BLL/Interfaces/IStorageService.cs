using Microsoft.AspNetCore.Http;

namespace AlgoHub.BLL.Interfaces;

public interface IStorageService
{
    Task SaveFile(IFormFile file, string name);
    Task UpdateFile(IFormFile newFile, string name);
    Task DeleteFile(string name);
}
