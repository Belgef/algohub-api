using AlgoHub.API.Models;

namespace AlgoHub.BLL.Interfaces;

public interface ICompilerService
{
    Task<CompilationResult?> Compile(string code, string language, string input);
}