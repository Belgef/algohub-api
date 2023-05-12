using AlgoHub.DAL.Entities;
using AlgoHub.DAL.Interfaces;
using AlgoHub.DAL.Repositories;
using FluentAssertions;
using NUnit.Framework;

namespace AlgoHub.DAL.IntegrationTests;

[NonParallelizable]
public class ProblemRepositoryTests
{
    private readonly IProblemRepository _problemRepository;

    public ProblemRepositoryTests()
    {
        _problemRepository = new ProblemRepository(AlgoHubDbContextFactory.Instance);
    }

    [SetUp]
    public void Setup() => AlgoHubDbContextFactory.RunStartScript();

    [Test]
    public async Task GetProblemById_WhenPassedValidData_GetsProblem()
    {
        var problem = await _problemRepository.GetProblemById(1);

        problem.Should().BeEquivalentTo(new Problem()
        {
            ProblemId = 1,
            ProblemName = "Problem 1",
            ProblemContent = "problem1.txt",
            Author = new User()
            {
                UserName = "user1",
                FullName = "John Doe",
                IconName = "icon1"
            },
            ImageName = "image1.png",
            Views = 0,
            Solves = 0,
            Upvotes = 0,
            Downvotes = 0,
            TimeLimitMs = 1000,
            MemoryLimitBytes = 1024,
            CreateDate = new DateTime(2001, 1, 1)
        });
    }

    [Test]
    public async Task GetProblemById_WhenPassedValidDataNoUser_GetsProblemWithNullUser()
    {
        var problem = await _problemRepository.GetProblemById(2);

        problem.Should().BeEquivalentTo(new Problem()
        {
            ProblemId = 2,
            ProblemName = "Problem 2",
            ProblemContent = "problem2.txt",
            Author = null,
            ImageName = "image2.png",
            Views = 0,
            Solves = 0,
            Upvotes = 0,
            Downvotes = 0,
            TimeLimitMs = 2000,
            MemoryLimitBytes = 2048,
            CreateDate = new DateTime(2002, 2, 2)
        });
    }

    [Test]
    public async Task GetProblemById_WhenPassedInvalidData_ReturnsNull()
    {
        var problem = await _problemRepository.GetProblemById(0);

        problem.Should().BeNull();
    }

    [Test]
    public async Task GetProblems_WhenPassedValidData_ReturnsProblems()
    {
        var problems = await _problemRepository.GetProblems();

        problems.Should().BeEquivalentTo(new[]{
            new Problem()
            {
                ProblemId = 1,
                ProblemName = "Problem 1",
                ProblemContent = "problem1.txt",
                Author = new User()
                {
                    UserName = "user1",
                    FullName = "John Doe",
                    IconName = "icon1"
                },
                ImageName = "image1.png",
                Views = 0,
                Solves = 0,
                Upvotes = 0,
                Downvotes = 0,
                TimeLimitMs = 1000,
                MemoryLimitBytes = 1024,
                CreateDate = new DateTime(2001, 1, 1)
            },
            new Problem()
            {
                ProblemId = 2,
                ProblemName = "Problem 2",
                ProblemContent = "problem2.txt",
                Author = null,
                ImageName = "image2.png",
                Views = 0,
                Solves = 0,
                Upvotes = 0,
                Downvotes = 0,
                TimeLimitMs = 2000,
                MemoryLimitBytes = 2048,
                CreateDate = new DateTime(2002, 2, 2)
            }
        });
    }

    [Test]
    public async Task AddProblem_WhenPassedValidData_AddsProblem()
    {
        Problem problem = new()
        {
            ProblemName = "newProblem",
            ProblemContent = "file",
            Author = new() { UserId = Guid.Parse("22222222-2222-2222-2222-222222222222") },
            TimeLimitMs = 20,
            MemoryLimitBytes = 40
        };

        var result = await _problemRepository.AddProblem(problem);

        result.Should().NotBeNull();

        var resultProblem = await _problemRepository.GetProblemById((int)result);

        resultProblem.Should().NotBeNull();

        resultProblem!.CreateDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(5));

        resultProblem.Should().BeEquivalentTo(new Problem()
        {
            ProblemId = result,
            ProblemName = problem.ProblemName,
            ProblemContent = problem.ProblemContent,
            Author = new User()
            {
                UserName = "user2",
                FullName = "Jane Doe",
                IconName = null
            },
            ImageName = problem.ImageName,
            Views = 0,
            Solves = 0,
            Upvotes = 0,
            Downvotes = 0,
            TimeLimitMs = problem.TimeLimitMs,
            MemoryLimitBytes = problem.MemoryLimitBytes
        }, (o) => o.Excluding(su => su.CreateDate));
    }
}
