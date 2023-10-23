using Library.project.Tabs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.project.Database
{
    public static class FakeDatabase
    {
        private static List<Person> people = new List<Person>();
        private static List<Course> course = new List<Course>();
        public static List<Person> People
        {
            get
            {
                return people;
            }
        }
        public static List<Course> Course
        {
            get
            {
                return course;
            }
        }
    }
}
