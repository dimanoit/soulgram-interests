using BenchmarkDotNet.Attributes;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Soulgram.Interests.Application;
using Soulgram.Interests.Application.Queries;
using Soulgram.Interests.Infrastructure;
using Soulgram.Interests.Persistence;

namespace Soulgram.Interests.Api.Benchmark;

[MemoryDiagnoser]
[SimpleJob(1, targetCount: 50)]
public class GetGeneralInterestsQueryBenchmark
{
    private readonly IMediator _mediator;

    public GetGeneralInterestsQueryBenchmark()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false);

        IConfiguration config = builder.Build();

        var services = new ServiceCollection();
        services.AddApplication(config);
        services.AddInfrastructure(config);
        services.AddPersistence(config);
        _mediator = services.BuildServiceProvider().GetService(typeof(IMediator)) as IMediator;
    }

    [Benchmark]
    public void GetInterestWithoutUsers()
    {
        _mediator.Send(new GetInterestQuery("629b6ba47040593d4221d1f9"))
            .GetAwaiter()
            .GetResult();
    }

    [Benchmark]
    public void GetInterestWithUsers()
    {
        _mediator.Send(new GetInterestQuery("629b6ba47040593d4221d1f8"))
            .GetAwaiter()
            .GetResult();
    }
}