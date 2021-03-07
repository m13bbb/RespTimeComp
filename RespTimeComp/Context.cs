using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace RespTimeComp
{
    public class Context : DbContext
    {
        private readonly string _conn;

        public Context(string conn)
        {
            _conn = conn;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_conn);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>().Property(x => x.StudentId).ValueGeneratedNever();

            modelBuilder.Entity<Student>().HasMany(x => x.Courses).WithOne().HasForeignKey(x => x.StudentID);

            modelBuilder.Entity<Course>().Property(x => x.CourseId).ValueGeneratedNever();

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }
    }
}