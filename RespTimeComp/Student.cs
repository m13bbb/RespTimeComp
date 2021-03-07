using System;
using System.Collections.Generic;
using System.Text;

namespace RespTimeComp
{
    public class Student
    {
        public int StudentId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public decimal GPA { get; set; }

        public List<Course> Courses = new List<Course>();
    }
}
