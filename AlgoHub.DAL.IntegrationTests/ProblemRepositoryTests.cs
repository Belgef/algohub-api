using AlgoHub.DAL.Entities;
using AlgoHub.DAL.Repositories;
using Dapper;
using FluentAssertions;
using NUnit.Framework;

namespace AlgoHub.DAL.IntegrationTests;

public class ProblemRepositoryTests
{
    private readonly ProblemRepository _problemRepository;

    public ProblemRepositoryTests()
    {
        _problemRepository = new(AlgoHubDbContextFactory.Instance);
    }

    [SetUp]
    public void Setup() => AlgoHubDbContextFactory.RunStartScript();

    [Test]
    public async Task GetsProblem()
    {
        var problem = await _problemRepository.GetProblemById(1);

        problem.Should().BeEquivalentTo(new ProblemDetailed()
        {
            ProblemId = 1,
            ProblemName = "Problem 1",
            ProblemContentFileName = "problem1.txt",
            AuthorName = "John Doe",
            AuthorUserName = "user1",
            AuthorIconName = "icon1",
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
}
