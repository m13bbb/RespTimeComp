using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using Bogus;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RespTimeComp
{
    public class Program
    {
        public static IConfigurationRoot _config;

        [GlobalSetup]
        public void GlobalSetup()
        {
            var builder = new ConfigurationBuilder()
                          .SetBasePath(AppContext.BaseDirectory)
                          .AddJsonFile("appsettings.json");

            _config = builder.Build();

            PrepareData();
        }

        [Benchmark]
        public void DapperGetAll()
        {
            string sql = "SELECT * FROM [Students]";

            using (var conn = new SqlConnection(_config["Connection"]))
            {
                var students = conn.Query<Student>(sql).ToList();
                //Console.WriteLine("Students Count:");
                //Console.WriteLine(students.Count);
            }
        }

        [Benchmark]
        public void EFCoreGetAll()
        {
            using (var ctx = new Context(_config["Connection"]))
            {
                var all_students = ctx.Students.ToList();
                //Console.WriteLine("Students Count:");
                //Console.WriteLine(all_students.Count());
            }
        }

        static void Main()
        {
            var summary = BenchmarkRunner.Run<Program>();
        }

        private static void PrepareData()
        {
            using (var ctx = new Context(_config["Connection"]))
            {
                if (ctx.Database.CanConnect())
                    return;

                ctx.Database.EnsureCreated();
                var startId = 0;
                var rnd = new Random();
                var students = new Faker<Student>()
                    .RuleFor(x => x.StudentId, _ => startId++)
                    .RuleFor(x => x.FirstName, f => f.Person.FirstName)
                    .RuleFor(x => x.LastName, f => f.Person.LastName)
                    .RuleFor(x => x.Email, f => f.Person.Email)
                    .RuleFor(x => x.GPA, _ => (decimal)((rnd.NextDouble() * 5) + 1))
                    .RuleFor(x => x.Courses, f =>
                    {
                        var list = new List<Course>();
                        for (int i = 0; i < rnd.Next(5); i++)
                        {
                            list.Add(new Course
                            {
                                Name = f.Commerce.Department()
                            });
                        }

                        return list;
                    })
                    .Generate(40_000);

                foreach (var student in students)
                {
                    ctx.Students.Add(student);
                    foreach (var course in student.Courses)
                        ctx.Courses.Add(course);
                }

                ctx.SaveChanges();
            }
        }
    }
}
