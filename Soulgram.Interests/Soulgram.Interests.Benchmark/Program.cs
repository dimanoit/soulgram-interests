// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Soulgram.Interests.Api.Benchmark;
using Soulgram.Interests.Application;
using Soulgram.Interests.Infrastructure;
using Soulgram.Interests.Persistence;



BenchmarkRunner.Run<GetGeneralInterestsQueryBenchmark>();

Console.WriteLine("Hello, World!");