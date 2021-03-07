using System;
using System.Collections.Generic;
using System.Text;

namespace RespTimeComp
{
    public class Course
    {
        public Guid CourseId { get; set; } = Guid.NewGuid();

        public string Name { get; set; }

        public int StudentID { get; set; }
    }
}
