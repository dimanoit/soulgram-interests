using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Soulgram.Interests.Application;

public static class ServiceInjector
{
    public static void AddApplication(this IServiceCollection services)
    {
        var currentAssembly = Assembly.GetExecutingAssembly();
        services.AddMediatR(currentAssembly);
    }
}