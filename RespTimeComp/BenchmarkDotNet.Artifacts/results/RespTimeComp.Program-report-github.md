``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
Intel Core i7-5820K CPU 3.30GHz (Broadwell), 1 CPU, 12 logical and 6 physical cores
.NET Core SDK=5.0.101
  [Host] : .NET Core 3.1.10 (CoreCLR 4.700.20.51601, CoreFX 4.700.20.51901), X64 RyuJIT


```
|       Method | Mean | Error |
|------------- |-----:|------:|
| DapperGetAll |   NA |    NA |
| EFCoreGetAll |   NA |    NA |

Benchmarks with issues:
  Program.DapperGetAll: DefaultJob
  Program.EFCoreGetAll: DefaultJob
