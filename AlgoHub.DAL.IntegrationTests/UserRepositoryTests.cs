using AlgoHub.DAL.Entities;
using AlgoHub.DAL.Interfaces;
using AlgoHub.DAL.Repositories;
using FluentAssertions;
using NUnit.Framework;

namespace AlgoHub.DAL.IntegrationTests;

[NonParallelizable]
public class UserRepositoryTests
{
    private readonly IUserRepository _userRepository;

    public UserRepositoryTests()
    {
        _userRepository = new UserRepository(AlgoHubDbContextFactory.Instance);
    }

    [SetUp]
    public void Setup() => AlgoHubDbContextFactory.RunStartScript();

    [Test]
    public async Task AddUser_WhenPassedUser_AddsUser()
    {
        var user = new User()
        {
            UserName = "new_user",
            FullName = "New User",
            Email = "email@email.com",
            PasswordHash = "somehash",
            PasswordSalt = "somesalt",
            IconName = "someIconName"
        };

        Guid? userId = await _userRepository.AddUser(user);

        Assert.That(userId, Is.Not.Null);

        user.UserId = userId;

        User? resultUser = await _userRepository.GetUserById((Guid)userId);

        Assert.That(resultUser, Is.Not.Null);

        Assert.That(resultUser.CreateDate, Is.Not.Null);

        Assert.That(resultUser.CreateDate, Is.InRange(DateTime.UtcNow.AddSeconds(-5), DateTime.UtcNow.AddSeconds(5)));

        resultUser.Should().BeEquivalentTo(new User()
        {
            UserId = userId,
            UserName = "new_user",
            FullName = "New User",
            Email = "email@email.com",
            IconName = "someIconName",
            CreateDate = resultUser.CreateDate
        });
    }

    [Test]
    public async Task AddUser_WhenPassedNullParams_AddsUser()
    {
        var user = new User()
        {
            UserName = "new_user",
            FullName = null,
            Email = "email@email.com",
            PasswordHash = "somehash",
            PasswordSalt = "somesalt",
            IconName = null
        };

        Guid? userId = await _userRepository.AddUser(user);

        Assert.That(userId, Is.Not.Null);

        user.UserId = userId;

        User? resultUser = await _userRepository.GetUserById((Guid)userId);

        Assert.That(resultUser, Is.Not.Null);

        Assert.That(resultUser.CreateDate, Is.Not.Null);

        Assert.That(resultUser.CreateDate, Is.InRange(DateTime.UtcNow.AddSeconds(-5), DateTime.UtcNow.AddSeconds(5)));

        resultUser.Should().BeEquivalentTo(new User()
        {
            UserId = userId,
            UserName = "new_user",
            FullName = null,
            Email = "email@email.com",
            IconName = null,
            CreateDate = resultUser.CreateDate
        });
    }

    [Test]
    public async Task LoginUser_WhenPassedValidData_ReturnsUserData()
    {
        var user = await _userRepository.LoginUser("user1", "hash1");

        user.Should().BeEquivalentTo(new User()
        {
            UserId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            UserName = "user1",
            FullName = "John Doe",
            Email = "user1@example.com",
            Role = new Role()
            {
                RoleId = 1,
                RoleName = "User"
            },
            IconName = "icon1",
            CreateDate = new DateTime(2001, 1, 1)
        });
    }

    [Test]
    public async Task GetUserSalt_WhenPassedValidData_ReturnsSalt()
    {
        var salt = await _userRepository.GetUserSalt("user1");

        Assert.That(salt, Is.EqualTo("salt1                                       "));
    }

    [TestCase("user1", false)]
    [TestCase("usernew", true)]
    public async Task CheckUserName_WhenPassedData_ReturnsResult(string userName, bool expectedResult)
    {
        bool actual = await _userRepository.CheckUserName(userName);

        Assert.That(actual, Is.EqualTo(expectedResult));
    }

    [TestCase("user2@example.com", false)]
    [TestCase("emailnew@gmail.com", true)]
    public async Task CheckEmail_WhenPassedData_ReturnsResult(string email, bool expectedResult)
    {
        bool actual = await _userRepository.CheckEmail(email);

        Assert.That(actual, Is.EqualTo(expectedResult));
    }
}
