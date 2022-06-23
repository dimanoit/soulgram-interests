using Microsoft.Extensions.DependencyInjection;

namespace Soulgram.Interests.Api.Benchmark;

public static class ServiceLocator
{
    public static ServiceProvider ServiceProvider { get; set; } = null!;
}