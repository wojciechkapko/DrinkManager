using System;
using BLL.Enums;

namespace BLL
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsPaid { get; set; }
        public TimeSpan Duration { get; set; }
        public Level Level { get; set; }
    }
}