using System;
using System.Collections.Generic;
using BLL;

namespace CourseManagerConsole
{
    internal class CourseCreator
    {
        private Course CreateCourse()
        {
            return new Course()
            {
                Duration = Utility.GetCourseDuration("Course duration [hh:mm:ss]: "),
                IsPaid = Utility.GetPaidInfo(),
                Level = Utility.GetDifficulty(),
                Name = Utility.GetGenericData("Course name: ")
            };
        }

        public void AddNewCourse(List<Course> courses)
        {
            var newCourse = CreateCourse();
            courses.Add(newCourse);
            Console.WriteLine($"\nNew course {newCourse.Name} added.\n");
        }
    }
}