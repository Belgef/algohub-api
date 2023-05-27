using AlgoHub.API.Models;
using AlgoHub.BLL.Interfaces;
using AlgoHub.BLL.Services;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Reflection;

namespace AlgoHub.BLL.IntegrationTests
{
    public class JDoodleTests
    {
        public ServiceProvider ServiceProvider { get; private set; }

        [OneTimeSetUp]
        public void Setup()
        {
            var serviceCollection = new ServiceCollection();
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(
                     path: "appsettings.json",
                     optional: false,
                     reloadOnChange: true)
                .AddUserSecrets(Assembly.GetExecutingAssembly())
               .Build();
            serviceCollection.AddSingleton<IConfiguration>(configuration);
            serviceCollection.AddHttpClient();
            serviceCollection.AddTransient<ICompilerService, JDoodleService>();

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        [Test]
        public async Task Compile_WhenPassedCorrectData_ReturnsResult()
        {
            ICompilerService compiler = ServiceProvider.GetService<ICompilerService>();

            var result = await compiler.Compile("print(\"Hello world\")", "python", "");

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Output, Is.EqualTo("Hello world\n"));
        }

        [Test]
        public async Task Compile_WhenPassedCorrectInputData_ReturnsResult()
        {
            ICompilerService compiler = ServiceProvider.GetService<ICompilerService>();

            var result = await compiler.Compile("print(input()+\"\\n1\")", "python", "Input");

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Output, Is.EqualTo("Input\n1\n"));
        }

        [Test]
        public async Task Compile_WhenPassedJavaScript_ReturnsResult()
        {
            ICompilerService compiler = ServiceProvider.GetService<ICompilerService>();

            var result = await compiler.Compile("console.log(\"Hello world\")", "javascript", "");
             
            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Output, Is.EqualTo("Hello world\n"));
        }
    }
}