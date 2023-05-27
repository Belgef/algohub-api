using AlgoHub.DAL.Entities;

namespace AlgoHub.DAL.Interfaces
{
    public interface ITestRepository
    {
        Task<int?> AddTest(Test test, int problemId);
        Task<Test[]> GetProblemTests(int problemId);
    }
}